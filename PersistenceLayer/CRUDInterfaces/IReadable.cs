using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceLayer.CRUDInterfaces
{
	public interface IReadable<Entity> where Entity : class 
	{
		public Entity GetByID<IDType>(IDType id);
		public IEnumerable<Entity> GetAll();
		public IEnumerable<Entity> GetAllByFilter(Func<Entity, bool> filter);
	}
}
