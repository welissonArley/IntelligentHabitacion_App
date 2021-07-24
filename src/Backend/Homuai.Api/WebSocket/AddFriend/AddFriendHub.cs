using Homuai.Application.UseCases.Friends.AddFriends;
using Homuai.Communication.Request;
using Homuai.Communication.Response;
using Homuai.Exception;
using Homuai.Exception.ExceptionsBase;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Homuai.Api.WebSocket.AddFriend
{
    /// <summary>
    /// 
    /// </summary>
    public class AddFriendHub : Hub
    {
        private readonly Broadcaster _broadcaster;
        private readonly IAddFriendUseCase _useCase;
        private readonly IHubContext<AddFriendHub> _hubContext;
        
        /// <summary>
        /// 
        /// </summary>
        public AddFriendHub(IHubContext<AddFriendHub> hubContext, IAddFriendUseCase useCase)
        {
            _useCase = useCase;
            _broadcaster = Broadcaster.Instance;
            _hubContext = hubContext;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userToken"></param>
        /// <returns></returns>
        public async Task GetCode(string userToken)
        {
            try
            {
                var response = await _useCase.GetCodeToAddFriend(userToken);

                _broadcaster.Add(Context.ConnectionId, response.AdminId, _hubContext);

                await Clients.Client(Context.ConnectionId).SendAsync("AvailableCode", response.Code);
            }
            catch (HomuaiException e)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ThrowError", e.Message);
            }
            catch
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ThrowError", ResourceTextException.UNKNOW_ERROR);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userToken"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task CodeWasRead(string userToken, string code)
        {
            try
            {
                var response = await _useCase.CodeWasRead(userToken, code);
                var context = _broadcaster.Get(response.AdminId);
                context.StopTimer();
                context.SetNewFriendInformations(response.Id, Context.ConnectionId);
                context.StartTimer();

                var informationsNewFriendToAdd = (ResponseFriendJson)response;
                await Clients.Client(context.GetAdminConnectionSocketId()).SendAsync("CodeWasRead", informationsNewFriendToAdd);
            }
            catch (HomuaiException e)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ThrowError", e.Message);
            }
            catch
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ThrowError", ResourceTextException.UNKNOW_ERROR);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task Decline()
        {
            var adminId = _broadcaster.GetAdminId(Context.ConnectionId);
            if (!string.IsNullOrWhiteSpace(adminId))
            {
                var context = _broadcaster.Get(adminId);
                context.StopTimer();
                await context.SendDeclinedFriendCandidate();
                await _broadcaster.Remove(Context.ConnectionId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestApprove"></param>
        /// <returns></returns>
        public async Task Approve(RequestApproveAddFriendJson requestApprove)
        {
            try
            {
                var adminId = _broadcaster.GetAdminId(Context.ConnectionId);
                if (!string.IsNullOrWhiteSpace(adminId))
                {
                    var context = _broadcaster.Get(adminId);
                    context.StopTimer();
                    var friendId = context.GetFriendId();
                    var connetionFriend = context.GetFriendConnectionSocketId();
                    await _useCase.ApproveFriend(adminId, friendId, requestApprove);
                    context.SetNewFriendInformations(null, null);
                    await Clients.Client(Context.ConnectionId).SendAsync("SuccessfullyApproved");
                    await Clients.Client(connetionFriend).SendAsync("SuccessfullyApproved");
                    await _broadcaster.Remove(Context.ConnectionId);
                }
            }
            catch (HomuaiException e)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ThrowError", e.Message);
            }
            catch
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ThrowError", ResourceTextException.UNKNOW_ERROR);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(System.Exception exception)
        {
            await Disconnect();
            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task Disconnect()
        {
            await _broadcaster.Remove(Context.ConnectionId);
        }
    }
}
