using System;
using System.Linq;
using System.Data;
using DataAccessLayer.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PersistenceLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PersistenceLayer.Repositories.ConcreteClasses
{
	public class BaseRepository<Entity> : IBaseRepository<Entity> 
		where Entity : class
	{
		public EntityEntry entiyEntity;
		protected readonly MarketContext _marketContext;

		public BaseRepository(MarketContext marketContext)
		{
			_marketContext = marketContext;
		}

		#region Create
		virtual public void Add(Entity entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}
			_marketContext.Set<Entity>().Add(entity);
		}

		virtual public async Task<Entity> AddAsync(Entity entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}
			await _marketContext.Set<Entity>().AddAsync(entity);
			return entity;
		}

		virtual public IEnumerable<Entity> AddRange(IEnumerable<Entity> entities)
		{
			if (entities == null)
			{
				throw new ArgumentNullException(nameof(entities));
			}
			_marketContext.Set<Entity>().AddRange(entities);
			return entities;
		}

		virtual public async Task<IEnumerable<Entity>> AddRangeAsync(IEnumerable<Entity> entities)
		{
			if (entities == null)
			{
				throw new ArgumentNullException(nameof(entities));
			}
			await _marketContext.Set<Entity>().AddRangeAsync(entities);
			return entities;
		}

		#endregion

		#region Read

		virtual public Entity GetByID<IDType>(IDType id)
		{
			return _marketContext.Set<Entity>().Find(id);
		}
		virtual public async Task<Entity> GetByIDAsync<IDType>(IDType id)
		{
			return await _marketContext.Set<Entity>().FindAsync(id);
		}

		virtual public IEnumerable<Entity> GetAllByFilter(Func<Entity, bool> filter)
		{
			return _marketContext.Set<Entity>().AsNoTracking().Where(filter).ToList().Reverse<Entity>();
		}
		virtual public IEnumerable<Entity> GetAll()
		{
			return _marketContext.Set<Entity>().AsNoTracking().ToList().Reverse<Entity>();
		}
		virtual public Entity GetByFilter(Func<Entity, bool> filter)
		{
			return _marketContext.Set<Entity>().AsNoTracking().SingleOrDefault(filter);
		}

		#endregion

		#region Update

		virtual public void Update<IDType>(IDType ID, Entity newEntity)
		{
			var entityToUpdate = _marketContext.Set<Entity>().Find(ID);

			if (entityToUpdate != null)
			{
				_marketContext.Entry(entityToUpdate).CurrentValues.SetValues(newEntity);
				_marketContext.SaveChanges();
			}
			else
			{
				throw new ArgumentNullException(nameof(entityToUpdate));
			}
		}

		#endregion

		#region Delete
		virtual public void Delete(Entity entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}
			_marketContext.Set<Entity>().Remove(entity);
		}

		virtual public void DeleteById(int id)
		{
			Entity entity = this.GetByID<int>(id);
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}
			this.Delete(entity);
		}



		virtual public void DeleteRange(IEnumerable<Entity> entities)
		{
			if (entities == null)
			{
				throw new ArgumentNullException(nameof(entities));
			}
			
			_marketContext.Set<Entity>().RemoveRange(entities);
		}

		#endregion

		#region Additional Functionalities

		virtual public bool Exist(Func<Entity, bool> filter)
		{
			return _marketContext.Set<Entity>().Any(filter);
		}

		virtual public IEnumerable<Entity> GetPage(int pageNumber, int recordsPerPage, Func<Entity, Key> primaryKey)
		{
			int startIndex = (pageNumber - 1) * recordsPerPage;
			int endIndex = pageNumber * recordsPerPage - 1;
			return _marketContext.Set<Entity>().OrderBy(primaryKey).Skip(startIndex).Take(recordsPerPage).ToList();
		}

		#endregion

	}
}
