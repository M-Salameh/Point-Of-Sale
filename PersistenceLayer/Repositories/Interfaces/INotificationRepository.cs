using DataAccessLayer.Models;
using System.Collections.Generic;

namespace PersistenceLayer.Repositories.Interfaces
{
	public interface INotificationRepository : IBaseRepository<Notification>
	{
		IEnumerable<Notification> GetLatestNotifications(int numberOfNotifications);
	}
}
