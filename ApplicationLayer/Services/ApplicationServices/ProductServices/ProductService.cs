using System;
using ApplicationLayer.DTOs;
using DataAccessLayer.Models;
using ApplicationLayer.AutoMapper;
using System.Collections.Generic;
using PersistenceLayer.UnitOfWork.Interfaces;
using ApplicationLayer.Services.ApplicationServices.BaseServices;

namespace ApplicationLayer.Services.ApplicationServices.ProductServices
{
	public class ProductService : BaseService , IProductService
	{
		public static AutoMapper<Product, ProductDTO> autoMapper;
		public ProductService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			autoMapper = new AutoMapper<Product, ProductDTO>();
		}

		#region Create

		public void AddProduct(ProductDTO productDTO)
		{
			Product product = autoMapper.Map(productDTO);
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;

			unitOfWork.ProductRepository.Add(product);
			unitOfWork.SaveChanges();
		}

		public void AddProductAsync(ProductDTO productDTO)
		{
			Product product = autoMapper.Map(productDTO);
			product.CreatedAt = DateTime.Now;
			product.UpdatedAt = DateTime.Now;

			unitOfWork.ProductRepository.AddAsync(product);
			unitOfWork.SaveChanges();
		}

		public void AddRangeOfProducts(IEnumerable<ProductDTO> productsDTO)
		{
			List<Product> products = new List<Product>();

			foreach (ProductDTO productDTO in productsDTO)
			{
				productDTO.CreatedAt = DateTime.Now;
				productDTO.UpdatedAt = DateTime.Now;
				products.Add(autoMapper.Map(productDTO));
			}
			unitOfWork.ProductRepository.AddRange(products);
			unitOfWork.SaveChanges();
		}

		public void AddRangeOfProductsAsync(IEnumerable<ProductDTO> productsDTO)
		{
			List<Product> products = new List<Product>();

			foreach (ProductDTO productDTO in productsDTO)
			{
				productDTO.CreatedAt = DateTime.Now;
				productDTO.UpdatedAt = DateTime.Now;
				products.Add(autoMapper.Map(productDTO));
			}
			unitOfWork.ProductRepository.AddRangeAsync(products);
			unitOfWork.SaveChanges();
		}

		#endregion

		#region Read

		public ProductDTO GetProductByID<IDType>(IDType id)
		{
			return autoMapper.Map(unitOfWork.ProductRepository.GetByID(id));
		}

		public ProductDTO GetProductByIDAsync<IDType>(IDType id)
		{
			return autoMapper.Map(unitOfWork.ProductRepository.GetByIDAsync(id).Result);
		}

		public ProductDTO GetProductByFilter(Func<ProductDTO, bool> filter)
		{
			Func<Product, bool> AdaptiveFilter = (product) => filter(autoMapper.Map(product));

			return autoMapper.Map(unitOfWork.ProductRepository.GetByFilter(AdaptiveFilter));
		}

		public IEnumerable<ProductDTO> GetAllProducts()
		{
			IEnumerable<Product> products = unitOfWork.ProductRepository.GetAll();

			List<ProductDTO> productsDTO = new List<ProductDTO>();

			unitOfWork.SaveChanges();

			foreach (Product product in products)
			{
				productsDTO.Add(autoMapper.Map(product));
			}

			return productsDTO;
		}

		public IEnumerable<ProductDTO> GetAllProductsByFilter(Func<ProductDTO, bool> filter)
		{

			Func<Product, bool> AdaptiveFilter = (product) => filter(autoMapper.Map(product));

			IEnumerable<Product> products = unitOfWork.ProductRepository.GetAllByFilter(AdaptiveFilter);

			unitOfWork.SaveChanges();

			List<ProductDTO> productsDTO = new List<ProductDTO>();

			foreach (Product product in products)
			{
				productsDTO.Add(autoMapper.Map(product));
			}

			return productsDTO;
		}

		public IEnumerable<ProductDTO> GetProductByPage(int pageNumber, int recordsPerPage)
		{
			IEnumerable<Product> products = unitOfWork.ProductRepository.GetPage(pageNumber, recordsPerPage);

			List<ProductDTO> productsDTO = new List<ProductDTO>();

			foreach (Product product in products)
			{
				productsDTO.Add(autoMapper.Map(product));
			}

			return productsDTO;

		}



		#endregion

		#region Update

		public void UpdateProduct<IDType>(IDType ID, ProductDTO newProductDTO)
		{
			newProductDTO.UpdatedAt = DateTime.Now;
			unitOfWork.ProductRepository.Update(ID, autoMapper.Map(newProductDTO));
			unitOfWork.SaveChanges();
		}

		#endregion

		#region Delete

		public void DeleteProduct(ProductDTO productDTO)
		{
			unitOfWork.ProductRepository.Delete(autoMapper.Map(productDTO));
			unitOfWork.SaveChanges();
		}

		public void DeleteRangeOfProducts(IEnumerable<ProductDTO> productsDTO)
		{
			List<Product> products = new List<Product>();

			foreach (ProductDTO productDTO in productsDTO)
			{
				products.Add(autoMapper.Map(productDTO));
			}
			unitOfWork.ProductRepository.DeleteRange(products);
			unitOfWork.SaveChanges();
		}

		public void DeleteProductById(int id)
		{

			Func<OrderItems , bool> OrderItemsFilter =
			(orderItems) => orderItems.ProductId == id;

			Func<EntryItems , bool> EntryItemsFilter =
			(entryItems) => entryItems.ProductId == id;

			if (unitOfWork.ProductRepository.GetByID<int>(id).OrderItems != null)
            {
                IEnumerable<OrderItems> orderItems = unitOfWork.OrderItemsRepository.GetAllByFilter(OrderItemsFilter);
				unitOfWork.OrderItemsRepository.DeleteRange(orderItems);
			}

			if (unitOfWork.ProductRepository.GetByID<int>(id).EntryItems != null)
            {
				IEnumerable<EntryItems> entryItems = unitOfWork.EntryItemsRepository.GetAllByFilter(EntryItemsFilter);
				unitOfWork.EntryItemsRepository.DeleteRange(entryItems);
            }

            unitOfWork.ProductRepository.DeleteById(id);

			unitOfWork.SaveChanges();
		}

		#endregion

		#region Additional Functionalities

		public bool Exist(int Id)
		{
			return unitOfWork.ProductRepository.Exist((product) => product.Id == Id);
		}
		
		public int AddProductAndGetId(ProductDTO productDTO)
		{

			Product product = autoMapper.Map(productDTO);
			product.CreatedAt = DateTime.Now;
			product.UpdatedAt = DateTime.Now;
			unitOfWork.ProductRepository.Add(product);
			unitOfWork.SaveChanges();
			return product.Id;
		
		}

		public IEnumerable<ProductDTO> GetProductsById(IEnumerable<int> ProductIdentifiers)
		{

			IEnumerable<Product> SelectedProducts = unitOfWork.ProductRepository.GetProductsById(ProductIdentifiers);
			unitOfWork.SaveChanges();
			List<ProductDTO> SelectedProductsAsDTOs = new List<ProductDTO>();
			foreach(Product product in SelectedProducts)
			{
				SelectedProductsAsDTOs.Add(autoMapper.Map(product));
			}
			return SelectedProductsAsDTOs;
			
		}

		#endregion

	}
}
