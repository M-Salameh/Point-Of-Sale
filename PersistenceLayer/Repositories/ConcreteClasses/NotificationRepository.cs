using DataAccessLayer.Models;
using PersistenceLayer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PersistenceLayer.Repositories.ConcreteClasses
{
	public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
	{
		public NotificationRepository(MarketContext marketContext)
			: base(marketContext)
		{
			
		}

		public IEnumerable<Notification> GetLatestNotifications(int numberOfNotifications)
		{
			return _marketContext.Notification.OrderByDescending((notification) => notification.BroadCastedAt).Take(numberOfNotifications);	
		}

	}
}