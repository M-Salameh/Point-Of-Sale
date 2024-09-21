using System;
using ApplicationLayer.DTOs;
using DataAccessLayer.Models;
using System.Collections.Generic;
using ApplicationLayer.AutoMapper;
using PersistenceLayer.UnitOfWork.Interfaces;
using ApplicationLayer.Services.ApplicationServices.BaseServices;

namespace ApplicationLayer.Services.ApplicationServices.ProductEntryServices
{
	public class ProductEntryService : BaseService, IProductEntryService
	{
		public static AutoMapper<ProductEntry, ProductEntryDTO> autoMapper;

		public ProductEntryService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			autoMapper = new AutoMapper<ProductEntry, ProductEntryDTO>();
		}

		#region Create

		public void AddProductEntry(ProductEntryDTO productEntryDTO)
		{
			
			if (!unitOfWork.SupplierRepository.Exist((supplier) => supplier.Id == productEntryDTO.SupplierId))
			{
				throw new MissingMemberException("There was no Supplier with the given Id");
			}

			ProductEntry productEntry = autoMapper.Map(productEntryDTO);
			productEntry.CreatedAt = DateTime.Now;
			productEntry.UpdatedAt = DateTime.Now;
			productEntry.Date = DateTime.Now;

			unitOfWork.ProductEntryRepository.Add(productEntry);
			unitOfWork.SaveChanges();
			
		}

		public void AddProductEntryAsync(ProductEntryDTO productEntryDTO)
		{
			if (!unitOfWork.SupplierRepository.Exist((supplier) => supplier.Id == productEntryDTO.SupplierId))
			{
				throw new MissingMemberException("There was no Supplier with the given Id");
			}

			ProductEntry productEntry = autoMapper.Map(productEntryDTO);
			productEntry.CreatedAt = DateTime.Now;
			productEntry.UpdatedAt = DateTime.Now;
			productEntry.Date = DateTime.Now;

			unitOfWork.ProductEntryRepository.AddAsync(productEntry);
			unitOfWork.SaveChanges();

		}

		public void AddRangeOfProductEntrys(IEnumerable<ProductEntryDTO> productEntrysDTO)
		{
			List<ProductEntry> productEntrys = new List<ProductEntry>();

			foreach (ProductEntryDTO productEntryDTO in productEntrysDTO)
			{
				if (unitOfWork.SupplierRepository.Exist((supplier) => supplier.Id == productEntryDTO.SupplierId))
				{
					productEntryDTO.CreatedAt = DateTime.Now;
					productEntryDTO.UpdatedAt = DateTime.Now;
					productEntryDTO.Date = DateTime.Now;
					productEntrys.Add(autoMapper.Map(productEntryDTO));
				}
			}

			if (productEntrys.Count == 0)
			{
				throw new MissingMemberException("There was no Supplier with the given Id");
			}
			
			unitOfWork.ProductEntryRepository.AddRange(productEntrys);
			unitOfWork.SaveChanges();
			
		}

		public void AddRangeOfProductEntrysAsync(IEnumerable<ProductEntryDTO> productEntrysDTO)
		{
			List<ProductEntry> productEntrys = new List<ProductEntry>();

			foreach (ProductEntryDTO productEntryDTO in productEntrysDTO)
			{
				if (unitOfWork.SupplierRepository.Exist((supplier) => supplier.Id == productEntryDTO.SupplierId))
				{
					productEntryDTO.CreatedAt = DateTime.Now;
					productEntryDTO.UpdatedAt = DateTime.Now;
					productEntryDTO.Date = DateTime.Now;
					productEntrys.Add(autoMapper.Map(productEntryDTO));
				}
			}

			if (productEntrys.Count == 0)
			{
				throw new MissingMemberException("There was no Supplier with the given Id");
			}

			unitOfWork.ProductEntryRepository.AddRangeAsync(productEntrys);
			unitOfWork.SaveChanges();

		}

		#endregion

		#region Read

		public ProductEntryDTO GetProductEntryByID<IDType>(IDType id)
		{
			return autoMapper.Map(unitOfWork.ProductEntryRepository.GetByID(id));
		}

		public ProductEntryDTO GetProductEntryByIDAsync<IDType>(IDType id)
		{
			return autoMapper.Map(unitOfWork.ProductEntryRepository.GetByIDAsync(id).Result);
		}

		public ProductEntryDTO GetProductEntryByFilter(Func<ProductEntryDTO, bool> filter)
		{
			Func<ProductEntry, bool> AdaptiveFilter = (productEntry) => filter(autoMapper.Map(productEntry));

			return autoMapper.Map(unitOfWork.ProductEntryRepository.GetByFilter(AdaptiveFilter));
		}

		public IEnumerable<ProductEntryDTO> GetAllProductEntrys()
		{
			IEnumerable<ProductEntry> productEntrys = unitOfWork.ProductEntryRepository.GetAll();

			List<ProductEntryDTO> productEntrysDTO = new List<ProductEntryDTO>();

			foreach (ProductEntry productEntry in productEntrys)
			{
				productEntrysDTO.Add(autoMapper.Map(productEntry));
			}

			return productEntrysDTO;
		}

		public IEnumerable<ProductEntryDTO> GetAllProductEntrysByFilter(Func<ProductEntryDTO, bool> filter)
		{

			Func<ProductEntry, bool> AdaptiveFilter = (productEntry) => filter(autoMapper.Map(productEntry));

			IEnumerable<ProductEntry> productEntrys = unitOfWork.ProductEntryRepository.GetAllByFilter(AdaptiveFilter);

			List<ProductEntryDTO> productEntrysDTO = new List<ProductEntryDTO>();

			foreach (ProductEntry productEntry in productEntrys)
			{
				productEntrysDTO.Add(autoMapper.Map(productEntry));
			}

			return productEntrysDTO;
		}

		#endregion

		#region Update

		public void UpdateProductEntry<IDType>(IDType ID, ProductEntryDTO newProductEntryDTO)
		{
			if (!unitOfWork.SupplierRepository.Exist((supplier) => supplier.Id == newProductEntryDTO.SupplierId))
			{
				throw new MissingMemberException("There was no Supplier with the given Id");
			}

			
			newProductEntryDTO.UpdatedAt = DateTime.Now;
			unitOfWork.ProductEntryRepository.Update(ID, autoMapper.Map(newProductEntryDTO));
			unitOfWork.SaveChanges();
		}

		#endregion

		#region Delete

		public void DeleteProductEntry(ProductEntryDTO productEntryDTO)
		{
			unitOfWork.ProductEntryRepository.Delete(autoMapper.Map(productEntryDTO));
			unitOfWork.SaveChanges();
		}

		public void DeleteRangeOfProductEntrys(IEnumerable<ProductEntryDTO> productEntrysDTO)
		{
			List<ProductEntry> productEntrys = new List<ProductEntry>();

			foreach (ProductEntryDTO productEntryDTO in productEntrysDTO)
			{
				productEntrys.Add(autoMapper.Map(productEntryDTO));
			}
			unitOfWork.ProductEntryRepository.DeleteRange(productEntrys);
			unitOfWork.SaveChanges();
		}

		public void DeleteProductEntryById(int id)
		{
			unitOfWork.ProductEntryRepository.DeleteById(id);
			unitOfWork.SaveChanges();
		}

		#endregion

		#region Additional Functionalities

		public bool Exist(int Id)
		{
			return unitOfWork.ProductEntryRepository.Exist((productEntry) => productEntry.Id == Id);
		}

		public ProductEntryDTO GetAllEntriesItems(int productEntryId)
        {
			ProductEntry productEntry = unitOfWork.ProductEntryRepository.GetByID<int>(productEntryId);

			ProductEntryDTO productEntryDTO = autoMapper.Map(productEntry);

			IEnumerable<EntryItems> entriesItems = unitOfWork.EntryItemsRepository.GetAllByFilter((ProductEntry) => ProductEntry.ProductEntryId == productEntryId);

			List<EntryItemsDTO> entriesItemsDTOs = new List<EntryItemsDTO>();

			Product product = new Product();

			EntryItemsDTO entryItemsDTO = new EntryItemsDTO();

			foreach(EntryItems entryItems in entriesItems)
            {

				product = unitOfWork.ProductRepository.GetByID<int>((int) entryItems.ProductId);

				entryItemsDTO = EntryItemsServices.EntryItemsService.autoMapper.Map(entryItems);

				entryItemsDTO.Product = ProductServices.ProductService.autoMapper.Map(product);

				entriesItemsDTOs.Add(entryItemsDTO);

			}

			productEntryDTO.EntryItems = entriesItemsDTOs;

			return productEntryDTO;
        }

		#endregion

	}
}
