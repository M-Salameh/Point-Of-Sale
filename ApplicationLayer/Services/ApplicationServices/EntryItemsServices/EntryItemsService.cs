using System;
using ApplicationLayer.DTOs;
using DataAccessLayer.Models;
using ApplicationLayer.AutoMapper;
using System.Collections.Generic;
using PersistenceLayer.UnitOfWork.Interfaces;
using ApplicationLayer.Services.ApplicationServices.BaseServices;

namespace ApplicationLayer.Services.ApplicationServices.EntryItemsServices
{
	public class EntryItemsService : BaseService, IEntryItemsService
	{

		public static AutoMapper<EntryItems, EntryItemsDTO> autoMapper;

		public EntryItemsService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			autoMapper = new AutoMapper<EntryItems, EntryItemsDTO>();
		}

		#region Create

		public void AddEntryItems(EntryItemsDTO entryItemsDTO)
		{
			
			if (!unitOfWork.ProductRepository.Exist((product) => product.Id == entryItemsDTO.ProductId))
			{
				throw new MissingMemberException("There was no Product with the given Id");
			}

			if (!unitOfWork.ProductEntryRepository.Exist((productEntry) => productEntry.Id == entryItemsDTO.ProductEntryId))
			{
				throw new MissingMemberException("There was no Product Entry with the given Id");
			}

			EntryItems entryItems = autoMapper.Map(entryItemsDTO);
			entryItems.CreatedAt = DateTime.Now;
			entryItems.UpdatedAt	 = DateTime.Now;

			unitOfWork.EntryItemsRepository.Add(entryItems);
			unitOfWork.SaveChanges();
		}

		public void AddEntryItemsAsync(EntryItemsDTO entryItemsDTO)
		{
			
			if (!unitOfWork.ProductRepository.Exist((product) => product.Id == entryItemsDTO.ProductId))
			{
				throw new MissingMemberException("There was no Product with the given Id");
			}

			if (!unitOfWork.ProductEntryRepository.Exist((productEntry) => productEntry.Id == entryItemsDTO.ProductEntryId))
			{
				throw new MissingMemberException("There was no Product Entry with the given Id");
			}

			EntryItems entryItems = autoMapper.Map(entryItemsDTO);
			entryItems.CreatedAt = DateTime.Now;
			entryItems.UpdatedAt = DateTime.Now;
			
			unitOfWork.EntryItemsRepository.AddAsync(entryItems);
			unitOfWork.SaveChanges();
		}

		public void AddRangeOfEntryItemss(IEnumerable<EntryItemsDTO> entryItemssDTO)
		{
			List<EntryItems> entryItems = new List<EntryItems>();

			bool productExists;
			bool productEntryExists;

			foreach (EntryItemsDTO entryItemsDTO in entryItemssDTO)
			{
				productExists = unitOfWork.ProductRepository.Exist((product) => product.Id == entryItemsDTO.ProductId);
				productEntryExists = unitOfWork.ProductEntryRepository.Exist((productEntry) => productEntry.Id == entryItemsDTO.ProductEntryId);

				if (productExists && productEntryExists)
				{
					entryItemsDTO.CreatedAt = DateTime.Now;
					entryItemsDTO.UpdatedAt = DateTime.Now;
					entryItems.Add(autoMapper.Map(entryItemsDTO));
				}
			}

			if (entryItems.Count == 0)
			{
				throw new MissingMemberException("There were no Entry items with a correct product or product entry");
			}

			unitOfWork.EntryItemsRepository.AddRange(entryItems);
			unitOfWork.SaveChanges();
		}

		public void AddRangeOfEntryItemssAsync(IEnumerable<EntryItemsDTO> entryItemssDTO)
		{
			List<EntryItems> entryItems = new List<EntryItems>();

			foreach (EntryItemsDTO entryItemsDTO in entryItemssDTO)
			{
				bool productExists = unitOfWork.ProductRepository.Exist((product) => product.Id == entryItemsDTO.ProductId);
				bool productEntryExists = unitOfWork.ProductEntryRepository.Exist((productEntry) => productEntry.Id == entryItemsDTO.ProductEntryId);
				if (productExists && productEntryExists)
				{
					entryItemsDTO.CreatedAt = DateTime.Now;
					entryItemsDTO.UpdatedAt = DateTime.Now;
					entryItems.Add(autoMapper.Map(entryItemsDTO));
				}
			}

			if (entryItems.Count == 0)
			{
				throw new MissingMemberException("There were no Entry items with a correct product or product entry");
			}

			unitOfWork.EntryItemsRepository.AddRangeAsync(entryItems);
			unitOfWork.SaveChanges();
		}

		#endregion

		#region Read

		public EntryItemsDTO GetEntryItemsByID<IDType>(IDType id)
		{
			return autoMapper.Map(unitOfWork.EntryItemsRepository.GetByID(id));
		}

		public EntryItemsDTO GetEntryItemsByIDAsync<IDType>(IDType id)
		{
			return autoMapper.Map(unitOfWork.EntryItemsRepository.GetByIDAsync(id).Result);
		}

		public EntryItemsDTO GetEntryItemsByFilter(Func<EntryItemsDTO, bool> filter)
		{
			Func<EntryItems, bool> AdaptiveFilter = (entryItems) => filter(autoMapper.Map(entryItems));

			return autoMapper.Map(unitOfWork.EntryItemsRepository.GetByFilter(AdaptiveFilter));
		}

		public IEnumerable<EntryItemsDTO> GetAllEntryItemss()
		{
			IEnumerable<EntryItems> entryItemss = unitOfWork.EntryItemsRepository.GetAll();

			List<EntryItemsDTO> entryItemssDTO = new List<EntryItemsDTO>();

			foreach (EntryItems entryItems in entryItemss)
			{
				entryItemssDTO.Add(autoMapper.Map(entryItems));
			}

			return entryItemssDTO;
		}

		public IEnumerable<EntryItemsDTO> GetAllEntryItemssByFilter(Func<EntryItemsDTO, bool> filter)
		{

			Func<EntryItems, bool> AdaptiveFilter = (entryItems) => filter(autoMapper.Map(entryItems));

			IEnumerable<EntryItems> entryItemss = unitOfWork.EntryItemsRepository.GetAllByFilter(AdaptiveFilter);

			List<EntryItemsDTO> entryItemssDTO = new List<EntryItemsDTO>();

			foreach (EntryItems entryItems in entryItemss)
			{
				entryItemssDTO.Add(autoMapper.Map(entryItems));
			}

			return entryItemssDTO;
		}

		#endregion

		#region Update

		public void UpdateEntryItems<IDType>(IDType ID, EntryItemsDTO newEntryItemsDTO)
		{
			if (!unitOfWork.ProductRepository.Exist((product) => product.Id == newEntryItemsDTO.ProductId))
			{
				throw new MissingMemberException("There was no Product with the given Id, failed to update");
			}

			if (!unitOfWork.ProductEntryRepository.Exist((productEntry) => productEntry.Id == newEntryItemsDTO.ProductEntryId))
			{
				throw new MissingMemberException("There was no Product Entry with the given Id");
			}

			newEntryItemsDTO.UpdatedAt = DateTime.Now;
			unitOfWork.EntryItemsRepository.Update(ID, autoMapper.Map(newEntryItemsDTO));
			unitOfWork.SaveChanges();
		}

		#endregion

		#region Delete

		public void DeleteEntryItems(EntryItemsDTO entryItemsDTO)
		{
			unitOfWork.EntryItemsRepository.Delete(autoMapper.Map(entryItemsDTO));
			unitOfWork.SaveChanges();
		}

		public void DeleteEntryItemsById(int id)
		{
			unitOfWork.EntryItemsRepository.DeleteById(id);
			unitOfWork.SaveChanges();
		}

		public void DeleteRangeOfEntryItemss(IEnumerable<EntryItemsDTO> entryItemssDTO)
		{
			List<EntryItems> entryItemss = new List<EntryItems>();

			foreach (EntryItemsDTO entryItemsDTO in entryItemssDTO)
			{
				entryItemss.Add(autoMapper.Map(entryItemsDTO));
			}
			unitOfWork.EntryItemsRepository.DeleteRange(entryItemss);
			unitOfWork.SaveChanges();
		}

		#endregion

		#region Additional Functionalities

		public bool Exist(int Id)
		{
			return unitOfWork.EntryItemsRepository.Exist((entryItems) => entryItems.Id == Id);
		}

		#endregion

	}
}
