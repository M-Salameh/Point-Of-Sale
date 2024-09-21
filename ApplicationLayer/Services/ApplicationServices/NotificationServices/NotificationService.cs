using System;
using DataAccessLayer.Models;
using System.Collections.Generic;
using ApplicationLayer.AutoMapper;
using PersistenceLayer.UnitOfWork.Interfaces;
using ApplicationLayer.Services.ApplicationServices.BaseServices;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ApplicationLayer.Services.ApplicationServices.NotificationServices
{
    public class NotificationService : BaseService, INotificationService
	{
		private readonly IHubContext<NotificationHub> _notificationHubContext;

		private AutoMapper<Notification, NotificationMessage> autoMapper;

		public NotificationService(IUnitOfWork unitOfWork, IHubContext<NotificationHub> hubContext)
			: base(unitOfWork)
		{
			autoMapper = new AutoMapper<Notification, NotificationMessage>();
			this._notificationHubContext = hubContext;
		}

        #region Create

        public void AddNotification(NotificationMessage notificationMessage)
		{
			Notification notification = autoMapper.Map(notificationMessage);
			notification.BroadCastedAt = DateTime.Now;
			unitOfWork.NotificationRepository.Add(notification);
			unitOfWork.SaveChanges();
		}

		#endregion

		#region Read

		public NotificationMessage GetNotificationByID<IDType>(IDType id)
		{
			return autoMapper.Map(unitOfWork.NotificationRepository.GetByID(id));
		}

		public NotificationMessage GetNotificationByIDAsync<IDType>(IDType id)
		{
			return autoMapper.Map(unitOfWork.NotificationRepository.GetByIDAsync(id).Result);
		}

		public NotificationMessage GetNotificationByFilter(Func<NotificationMessage, bool> filter)
		{
			Func<Notification, bool> AdaptiveFilter = (notification) => filter(autoMapper.Map(notification));

			return autoMapper.Map(unitOfWork.NotificationRepository.GetByFilter(AdaptiveFilter));
		}

		public IEnumerable<NotificationMessage> GetAllNotifications()
		{
			IEnumerable<Notification> notifications = unitOfWork.NotificationRepository.GetAll();

			List<NotificationMessage> notificationsDTO = new List<NotificationMessage>();

			foreach (Notification notification in notifications)
			{
				notificationsDTO.Add(autoMapper.Map(notification));
			}

			return notificationsDTO;
		}

		public IEnumerable<NotificationMessage> GetAllNotificationsByFilter(Func<NotificationMessage, bool> filter)
		{

			Func<Notification, bool> AdaptiveFilter = (notification) => filter(autoMapper.Map(notification));

			IEnumerable<Notification> notifications = unitOfWork.NotificationRepository.GetAllByFilter(AdaptiveFilter);

			List<NotificationMessage> notificationsDTO = new List<NotificationMessage>();

			foreach (Notification notification in notifications)
			{
				notificationsDTO.Add(autoMapper.Map(notification));
			}

			return notificationsDTO;
		}

		public IEnumerable<NotificationMessage> GetLatestNotificationMessages(int numberOfNotificationsRequested)
        {

			IEnumerable<Notification> notifications = unitOfWork.NotificationRepository.GetLatestNotifications(numberOfNotificationsRequested);
			
			List<NotificationMessage> notificationMessages = new List<NotificationMessage>();

			foreach(Notification message in notifications)
            {
				notificationMessages.Add(autoMapper.Map(message));
            }
			
			return notificationMessages;

		}

        #endregion

        #region Send

		public async Task SendToAllAsync(NotificationMessage notificationMessage)
        {
			await _notificationHubContext.Clients.All.SendAsync("SendToPOSClerck", notificationMessage.MessageSubject, notificationMessage.MessageContent, notificationMessage.BroadCastedAt.ToString());
		}

        #endregion

    }
}
