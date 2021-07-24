using Homuai.Exception;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Timers;

namespace Homuai.Api.WebSocket.AddFriend
{
    /// <summary>
    /// 
    /// </summary>
    public class ContextModel
    {
        private readonly IHubContext<AddFriendHub> _hubContext;
        private readonly string _adminConnectionSocketId;
        private string _newFriendId { get; set; }
        private string _newFriendConnectionSocketId { get; set; }

        private short _secondTimer { get; set; }
        private Timer _timer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hubContext"></param>
        /// <param name="adminConnectionSocketId"></param>
        public ContextModel(IHubContext<AddFriendHub> hubContext, string adminConnectionSocketId)
        {
            _hubContext = hubContext;
            _adminConnectionSocketId = adminConnectionSocketId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetAdminConnectionSocketId()
        {
            return _adminConnectionSocketId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task SendErrorConnectionLostFriendCandidate()
        {
            if (!string.IsNullOrWhiteSpace(_newFriendConnectionSocketId))
                await _hubContext.Clients.Client(_newFriendConnectionSocketId).SendAsync("ThrowError", ResourceTextException.CONNECTION_ADMINISTRATOR_LOST);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task SendDeclinedFriendCandidate()
        {
            await _hubContext.Clients.Client(_newFriendConnectionSocketId).SendAsync("Declined");
            _newFriendConnectionSocketId = null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetFriendId()
        {
            return _newFriendId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetFriendConnectionSocketId()
        {
            return _newFriendConnectionSocketId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newFriendId"></param>
        /// <param name="newFriendConnectionSocketId"></param>
        public void SetNewFriendInformations(string newFriendId, string newFriendConnectionSocketId)
        {
            _newFriendId = newFriendId;
            _newFriendConnectionSocketId = newFriendConnectionSocketId;
        }
        /// <summary>
        /// 
        /// </summary>
        public void StopTimer()
        {
            _timer?.Stop();
            _timer?.Dispose();
            _timer = null;
        }
        /// <summary>
        /// 
        /// </summary>
        public void StartTimer()
        {
            _secondTimer = 60;
            _timer = new Timer(1000)
            {
                Enabled = false
            };
            _timer.Elapsed += ElapsedTimer;
            _timer.Enabled = true;
        }
        private async void ElapsedTimer(object sender, ElapsedEventArgs e)
        {
            if (_secondTimer >= 0)
                await _hubContext.Clients.Client(_adminConnectionSocketId).SendAsync("AvailableTime", _secondTimer--);
            else
                StopTimer();
        }
    }
}
