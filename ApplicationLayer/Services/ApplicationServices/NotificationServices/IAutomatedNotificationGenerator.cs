using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLayer.Services.ApplicationServices.NotificationServices
{
	public interface IAutomatedNotificationGenerator
	{
		void SendAutomatedNotification(NotificationMessage notificationMessage);
	}
}
