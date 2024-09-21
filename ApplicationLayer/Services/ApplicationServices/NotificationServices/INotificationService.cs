using ApplicationLayer.Services.ApplicationServices.BaseServices;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.ApplicationServices.NotificationServices
{
	public interface INotificationService : IBaseService
	{

		#region Create

		void AddNotification(NotificationMessage notificationMessage);

		#endregion

		#region Read

		NotificationMessage GetNotificationByID<IDType>(IDType id);
		NotificationMessage GetNotificationByIDAsync<IDType>(IDType id);
		NotificationMessage GetNotificationByFilter(Func<NotificationMessage, bool> filter);
		IEnumerable<NotificationMessage> GetAllNotifications();
		IEnumerable<NotificationMessage> GetAllNotificationsByFilter(Func<NotificationMessage, bool> filter);
		IEnumerable<NotificationMessage> GetLatestNotificationMessages(int numberOfNotificationsRequested);

		#endregion

		#region Send

		Task SendToAllAsync(NotificationMessage notificationMessage);

		#endregion

	}
}
