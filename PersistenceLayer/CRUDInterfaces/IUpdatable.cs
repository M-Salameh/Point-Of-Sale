using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceLayer.CRUDInterfaces
{
	public interface IUpdatable<Entity> where Entity : class
	{
		public void Update<IDType>(IDType ID, Entity newEntity);
	}
}
