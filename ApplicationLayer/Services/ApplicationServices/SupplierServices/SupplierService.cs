using System;
using ApplicationLayer.DTOs;
using DataAccessLayer.Models;
using System.Collections.Generic;
using ApplicationLayer.AutoMapper;
using PersistenceLayer.UnitOfWork.Interfaces;
using ApplicationLayer.Services.ApplicationServices.BaseServices;


namespace ApplicationLayer.Services.ApplicationServices.SupplierServices
{
	public class SupplierService : BaseService, ISupplierService
	{
		public static AutoMapper<Supplier, SupplierDTO> autoMapper;

		public SupplierService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			autoMapper = new AutoMapper<Supplier, SupplierDTO>();
		}

		#region Create

		public void AddSupplier(SupplierDTO supplierDTO)
		{
			Supplier supplier = autoMapper.Map(supplierDTO);
			supplier.CreatedAt = DateTime.Now;
			supplier.UpdatedAt = DateTime.Now;

			unitOfWork.SupplierRepository.Add(supplier);
			unitOfWork.SaveChanges();
		}

		public void AddSupplierAsync(SupplierDTO supplierDTO)
		{
			Supplier supplier = autoMapper.Map(supplierDTO);
			supplier.CreatedAt = DateTime.Now;
			supplier.UpdatedAt = DateTime.Now;

			unitOfWork.SupplierRepository.AddAsync(supplier);
			unitOfWork.SaveChanges();
		}

		public void AddRangeOfSuppliers(IEnumerable<SupplierDTO> suppliersDTO)
		{
			List<Supplier> suppliers = new List<Supplier>();

			foreach (SupplierDTO supplierDTO in suppliersDTO)
			{
				supplierDTO.CreatedAt = DateTime.Now;
				supplierDTO.UpdatedAt = DateTime.Now;
				suppliers.Add(autoMapper.Map(supplierDTO));
			}

			unitOfWork.SupplierRepository.AddRange(suppliers);
			unitOfWork.SaveChanges();
		}

		public void AddRangeOfSuppliersAsync(IEnumerable<SupplierDTO> suppliersDTO)
		{
			List<Supplier> suppliers = new List<Supplier>();

			foreach (SupplierDTO supplierDTO in suppliersDTO)
			{
				supplierDTO.CreatedAt = DateTime.Now;
				supplierDTO.UpdatedAt = DateTime.Now;
				suppliers.Add(autoMapper.Map(supplierDTO));
			}

			unitOfWork.SupplierRepository.AddRangeAsync(suppliers);
			unitOfWork.SaveChanges();
		}

		#endregion

		#region Read

		public SupplierDTO GetSupplierByID<IDType>(IDType id)
		{
			return autoMapper.Map(unitOfWork.SupplierRepository.GetByID(id));
		}

		public SupplierDTO GetSupplierByIDAsync<IDType>(IDType id)
		{
			return autoMapper.Map(unitOfWork.SupplierRepository.GetByIDAsync(id).Result);
		}

		public SupplierDTO GetSupplierByFilter(Func<SupplierDTO, bool> filter)
		{
			Func<Supplier, bool> AdaptiveFilter = (supplier) => filter(autoMapper.Map(supplier));

			return autoMapper.Map(unitOfWork.SupplierRepository.GetByFilter(AdaptiveFilter));
		}

		public IEnumerable<SupplierDTO> GetAllSuppliers()
		{
			IEnumerable<Supplier> suppliers = unitOfWork.SupplierRepository.GetAll();

			List<SupplierDTO> suppliersDTO = new List<SupplierDTO>();

			foreach (Supplier supplier in suppliers)
			{
				suppliersDTO.Add(autoMapper.Map(supplier));
			}

			return suppliersDTO;
		}

		public IEnumerable<SupplierDTO> GetAllSuppliersByFilter(Func<SupplierDTO, bool> filter)
		{

			Func<Supplier, bool> AdaptiveFilter = (supplier) => filter(autoMapper.Map(supplier));

			IEnumerable<Supplier> suppliers = unitOfWork.SupplierRepository.GetAllByFilter(AdaptiveFilter);

			List<SupplierDTO> suppliersDTO = new List<SupplierDTO>();

			foreach (Supplier supplier in suppliers)
			{
				suppliersDTO.Add(autoMapper.Map(supplier));
			}

			return suppliersDTO;
		}

		#endregion

		#region Update

		public void UpdateSupplier<IDType>(IDType ID, SupplierDTO newSupplierDTO)
		{

			newSupplierDTO.UpdatedAt = DateTime.Now;
			unitOfWork.SupplierRepository.Update(ID, autoMapper.Map(newSupplierDTO));
			unitOfWork.SaveChanges();

		}

		#endregion

		#region Delete

		public void DeleteSupplier(SupplierDTO supplierDTO)
		{
			unitOfWork.SupplierRepository.Delete(autoMapper.Map(supplierDTO));
			unitOfWork.SaveChanges();
		}

		public void DeleteSupplierById(int id)
		{
			Func<ProductEntry, bool> productEntryFilter =
			(productEntry) => productEntry.SupplierId == id;

			IEnumerable<ProductEntry> productEntries = unitOfWork.ProductEntryRepository.GetAllByFilter(productEntryFilter);
			
			IEnumerable<EntryItems> entriesItems = new List<EntryItems>();

			foreach (ProductEntry productEntry in productEntries)
			{

				entriesItems = unitOfWork.EntryItemsRepository.GetAllByFilter((entryItems) => entryItems.ProductEntryId == productEntry.Id);

				unitOfWork.EntryItemsRepository.DeleteRange(entriesItems);

			}

			unitOfWork.ProductEntryRepository.DeleteRange(productEntries);

			unitOfWork.SupplierRepository.DeleteById(id);

			unitOfWork.SaveChanges();
		}

		public void DeleteRangeOfSuppliers(IEnumerable<SupplierDTO> suppliersDTO)
		{
			List<Supplier> suppliers = new List<Supplier>();

			foreach (SupplierDTO supplierDTO in suppliersDTO)
			{
				suppliers.Add(autoMapper.Map(supplierDTO));
			}
			unitOfWork.SupplierRepository.DeleteRange(suppliers);
			unitOfWork.SaveChanges();
		}

		#endregion

		#region Additional Functionalities

		public bool Exist(int Id)
		{
			return unitOfWork.SupplierRepository.Exist((supplier) => supplier.Id == Id);
		}
		
		public void AddProducts(int supplierId, int productId, int quantity, int price)
		{
			ProductEntry productEntry = new ProductEntry { SupplierId = supplierId, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Date = DateTime.Now};
			
			unitOfWork.ProductEntryRepository.Add(productEntry);
			unitOfWork.SaveChanges();

			EntryItems entryItems = new EntryItems
			{
				ProductEntryId = productEntry.Id,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				ProductId = productId,
				Quantity = quantity,
				Price = price
			};

			unitOfWork.EntryItemsRepository.Add(entryItems);

			Product product = unitOfWork.ProductRepository.GetByFilter((product) => product.Id == productId);
			product.Quantity += quantity;

			product.UpdatedAt = DateTime.Now;
			unitOfWork.ProductRepository.Update<int>(productId, product);
			unitOfWork.SaveChanges();

		}

		public int AddSupplierAndGetId(SupplierDTO supplierDTO)
		{
			
			Supplier supplier = autoMapper.Map(supplierDTO);
			supplier.CreatedAt = DateTime.Now;
			supplier.UpdatedAt = DateTime.Now;

			unitOfWork.SupplierRepository.Add(supplier);
			unitOfWork.SaveChanges();
			return supplier.Id;

		}

		public SupplierDTO GetAllProductEntries(int supplierId)
		{
			Supplier supplier = unitOfWork.SupplierRepository.GetByID<int>(supplierId);

			SupplierDTO supplierDTO = autoMapper.Map(supplier);

			IEnumerable<ProductEntry> productEntries = unitOfWork.ProductEntryRepository.GetAllByFilter((ProductEntry) => ProductEntry.SupplierId == supplierId);

			List<ProductEntryDTO> productEntriesDTO = new List<ProductEntryDTO>();

			foreach (ProductEntry productEntry in productEntries)
			{
				productEntriesDTO.Add(ProductEntryServices.ProductEntryService.autoMapper.Map(productEntry));
			}

			supplierDTO.ProductEntry = productEntriesDTO;

			return supplierDTO;
		}

		#endregion

	}
}
