using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceLayer.CRUDInterfaces
{
	public interface ICRUDBasicOperations<Entity> : ICreatable<Entity>, IReadable<Entity>, IUpdatable<Entity>, IDeletable<Entity> where Entity : class 
	{

	}
}