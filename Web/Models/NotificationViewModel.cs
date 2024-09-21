using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class NotificationViewModel
    {
        public NotificationViewModel() { }
        public NotificationViewModel(string title, string message)
        {
            MessageSubject = title;
            MessageContent = message;
        }

        public NotificationViewModel(string title, string message, DateTime broadCastedAt)
        {
            MessageSubject = title;
            MessageContent = message;
            BroadCastedAt = broadCastedAt;
        }

        [Required (ErrorMessage = "Please Give Me The Subject Of the Message")]
        [Display(Name = "Subject")]
        public string MessageSubject { get; set; }

        [Required(ErrorMessage = "Please Give Me The Content Of the Message")]
        [Display(Name = "Content")]
        public string MessageContent { get; set; }

        [Display(Name = "BroadCastedAt")]
        public DateTime BroadCastedAt { get; set; }
    }
}
