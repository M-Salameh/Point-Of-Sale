using System;
using System.Text;
using ApplicationLayer.DTOs;
using DataAccessLayer.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApplicationLayer.Services.ApplicationServices.BaseServices;


namespace ApplicationLayer.Services.ApplicationServices.ProductServices
{
    public interface IProductService : IBaseService
    {
        #region Create

        void AddProduct(ProductDTO productDTO);
        void AddProductAsync(ProductDTO productDTO);
        void AddRangeOfProducts(IEnumerable<ProductDTO> productsDTO);
        void AddRangeOfProductsAsync(IEnumerable<ProductDTO> productsDTO);

        #endregion

        #region Read

        ProductDTO GetProductByID<IDType>(IDType id);
        ProductDTO GetProductByIDAsync<IDType>(IDType id);
        ProductDTO GetProductByFilter(Func<ProductDTO, bool> filter);
        IEnumerable<ProductDTO> GetAllProducts();
        IEnumerable<ProductDTO> GetAllProductsByFilter(Func<ProductDTO, bool> filter);
        IEnumerable<ProductDTO> GetProductByPage(int pageNumber, int recordsPerPage);
        #endregion

        #region Update
        public void UpdateProduct<IDType>(IDType ID, ProductDTO newProduct);

        #endregion

        #region Delete
        void DeleteProduct(ProductDTO ProductDTO);
        void DeleteProductById(int id);
        void DeleteRangeOfProducts(IEnumerable<ProductDTO> productsDTO);

        #endregion

        #region Additional Functionalities

        bool Exist(int Id);
        int AddProductAndGetId(ProductDTO productDTO);
        IEnumerable<ProductDTO> GetProductsById(IEnumerable<int> ProductIdentifiers);

        #endregion
    }
}
