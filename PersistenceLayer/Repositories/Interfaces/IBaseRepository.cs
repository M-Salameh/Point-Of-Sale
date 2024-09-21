using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PersistenceLayer.Repositories.Interfaces
{
	public interface IBaseRepository<Entity> 
		where Entity : class 
	{

		#region Create
		void Add(Entity entity);
		Task<Entity> AddAsync(Entity entity);
		IEnumerable<Entity> AddRange(IEnumerable<Entity> entities);
		Task<IEnumerable<Entity>> AddRangeAsync(IEnumerable<Entity> entities);
		
		#endregion

		#region Read

		public Entity GetByID<IDType>(IDType id);
		public Task<Entity> GetByIDAsync<IDType>(IDType id);
		public Entity GetByFilter(Func<Entity, bool> filter);
		public IEnumerable<Entity> GetAll();
		public IEnumerable<Entity> GetAllByFilter(Func<Entity, bool> filter);

		#endregion

		#region Update
		public void Update<IDType>(IDType ID, Entity newEntity);

		#endregion

		#region Delete
		public void Delete(Entity entity);
		public void DeleteById(int id);
		public void DeleteRange(IEnumerable<Entity> entities);

		#endregion

		#region Additional Functionalities

		bool Exist(Func<Entity, bool> filter);
		public IEnumerable<Entity> GetPage(int pageNumber, int recordsPerPage, Func<Entity, Key> primaryKey);

		#endregion

	}
}