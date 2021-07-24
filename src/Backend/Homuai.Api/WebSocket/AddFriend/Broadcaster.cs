using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Homuai.Api.WebSocket.AddFriend
{
    /// <summary>
    /// 
    /// </summary>
    public class Broadcaster
    {
        private readonly static Lazy<Broadcaster> _instance = new Lazy<Broadcaster>(() => new Broadcaster());

        private ConcurrentDictionary<string, object> _dictionary { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static Broadcaster Instance { get { return _instance.Value; } }

        /// <summary>
        /// 
        /// </summary>
        public Broadcaster()
        {
            _dictionary = new ConcurrentDictionary<string, object>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adminConnectionSocketId"></param>
        /// <param name="adminId"></param>
        /// <param name="hubContext"></param>
        public void Add(string adminConnectionSocketId, string adminId, IHubContext<AddFriendHub> hubContext)
        {
            _dictionary.TryAdd(adminConnectionSocketId, adminId);
            var context = new ContextModel(hubContext, adminConnectionSocketId);
            _dictionary.TryAdd(adminId, context);
            context.StartTimer();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adminConnectionSocketId"></param>
        /// <returns></returns>
        public async Task Remove(string adminConnectionSocketId)
        {
            if (_dictionary.ContainsKey(adminConnectionSocketId))
            {
                var adminId = _dictionary[adminConnectionSocketId].ToString();
                var context = (ContextModel)_dictionary[adminId];
                context.StopTimer();
                await context.SendErrorConnectionLostFriendCandidate();
                _dictionary.TryRemove(adminConnectionSocketId, out object value);
                _dictionary.TryRemove(adminId, out value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public ContextModel Get(string adminId)
        {
            _dictionary.TryGetValue(adminId, out object context);

            return (ContextModel)context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adminConnectionSocketId"></param>
        /// <returns></returns>
        public string GetAdminId(string adminConnectionSocketId)
        {
            return _dictionary.ContainsKey(adminConnectionSocketId) ? _dictionary[adminConnectionSocketId].ToString() : null;
        }
    }
}
