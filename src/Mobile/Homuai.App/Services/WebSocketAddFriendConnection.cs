using Homuai.App.Services.Communication;
using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Homuai.App.Services
{
    public class WebSocketAddFriendConnection
    {
        private readonly HubConnection _connection;
        private ICommand _callbackWhenAnErrorOccurs;
        private ICommand _callbackWhenTimeChange;

        public WebSocketAddFriendConnection()
        {
            _connection = new HubConnectionBuilder()
                    .WithUrl(new Uri($"wss://{RestEndPoints.BaseUrl}/addNewFriend"), HttpTransportType.WebSockets)
                    .WithAutomaticReconnect().Build();

            _connection.On<string>("ThrowError", (messageError) =>
            {
                _callbackWhenAnErrorOccurs?.Execute(messageError);
            });
            _connection.On<int>("AvailableTime", (timer) =>
            {
                _callbackWhenTimeChange?.Execute(timer);
            });
        }

        public void SetCallbacks(ICommand callbackWhenAnErrorOccurs, ICommand callbackWhenTimeChange)
        {
            _callbackWhenAnErrorOccurs = callbackWhenAnErrorOccurs;
            _callbackWhenTimeChange = callbackWhenTimeChange;
        }

        public async Task GetQrCodeToAddFriend(ICommand callbackCodeIsReceived, ICommand callbackWhenCodeIsRead, string token)
        {
            _connection.On<string>("AvailableCode", (code) =>
            {
                callbackCodeIsReceived?.Execute(code);
            });
            _connection.On<ResponseFriendJson>("CodeWasRead", (newFriendInformations) =>
            {
                callbackWhenCodeIsRead?.Execute(newFriendInformations);
            });
            await _connection.StartAsync();
            await _connection.InvokeAsync("GetCode", token);
        }
        public async Task QrCodeWasRead(ICommand callbackAdiminDeclined, ICommand callbackAdiminApproved, string token, string code)
        {
            _connection.On("Declined", () =>
            {
                callbackAdiminDeclined?.Execute(null);
            });
            _connection.On("SuccessfullyApproved", () =>
            {
                callbackAdiminApproved?.Execute(null);
            });
            await _connection.StartAsync();
            await _connection.InvokeAsync("CodeWasRead", token, code);
        }
        public async Task DeclinedFriendCandidate()
        {
            await _connection.InvokeAsync("Decline").ConfigureAwait(false);
        }
        public async Task ApproveFriendCandidate(ICommand callbackSuccessfullyApproved, RequestApproveAddFriendJson requestApprove)
        {
            _connection.On("SuccessfullyApproved", () =>
            {
                callbackSuccessfullyApproved?.Execute(null);
            });
            await _connection.InvokeAsync("Approve", requestApprove).ConfigureAwait(false);
        }
        public async Task StopConnection()
        {
            await _connection.StopAsync().ConfigureAwait(false);
            await _connection.DisposeAsync().ConfigureAwait(false);
        }
    }
}
