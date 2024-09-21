
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.ApplicationServices.NotificationServices
{
	public class NotificationHub : Hub
	{
		public async Task SendNotification(string message)
		{
			await Clients.All.SendAsync("SendToPOSClerck", message);
		}
	}
	public class NotificationMessage
	{

		public string MessageSubject { get; set; }
		public string MessageContent { get; set; }
		public DateTime? BroadCastedAt { get; set; } 

	}
}