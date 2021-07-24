using System.Collections.Generic;

namespace Homuai.Communication.Response
{
    public class ResponseTaskJson
    {
        public ResponseTaskJson()
        {
            Assign = new List<ResponseUserSimplifiedJson>();
        }

        public string IdTaskToRegisterRoomCleaning { get; set; }
        public string Room { get; set; }
        public bool CanRate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanCompletedToday { get; set; }
        public IList<ResponseUserSimplifiedJson> Assign { get; set; }
    }
}
