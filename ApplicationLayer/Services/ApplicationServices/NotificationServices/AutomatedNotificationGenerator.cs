using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLayer.Services.ApplicationServices.NotificationServices
{
	public class AutomatedNotificationGenerator : IAutomatedNotificationGenerator
	{
		public INotificationService NotificationService { get; set; }

		public AutomatedNotificationGenerator(INotificationService NotificationService)
		{
			this.NotificationService = NotificationService;
		}

		public async void SendAutomatedNotification(NotificationMessage notificationMessage)
		{
			NotificationService.AddNotification(notificationMessage);
			await NotificationService.SendToAllAsync(notificationMessage);
		}

	}
}
