using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceLayer.CRUDInterfaces
{
	public interface IDeletable<Entity> where Entity : class
	{
		public void Delete(Entity entity);
	}
}
