using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceLayer.CRUDInterfaces
{
	public interface ICreatable<Entity> where Entity : class
	{
		public void Add(Entity entity);
	}
}
