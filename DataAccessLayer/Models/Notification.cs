using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Notification
    {

        public int Id { get; set; }
        public string MessageSubject { get; set; }
        public string MessageContent { get; set; }
        public DateTime? BroadCastedAt { get; set; }

    }
}
