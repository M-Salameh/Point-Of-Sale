using System;
using System.Text;
using System.Collections.Generic;
using ApplicationLayer.Services.ApplicationServices.BaseServices;
using ApplicationLayer.DTOs;

namespace ApplicationLayer.Services.ApplicationServices.ProductEntryServices
{
    public interface IProductEntryService : IBaseService
    {
        #region Create

        void AddProductEntry(ProductEntryDTO productEntryDTO);
        void AddProductEntryAsync(ProductEntryDTO productEntryDTO);
        void AddRangeOfProductEntrys(IEnumerable<ProductEntryDTO> productEntrysDTO);
        void AddRangeOfProductEntrysAsync(IEnumerable<ProductEntryDTO> productEntrysDTO);

        #endregion

        #region Read

        ProductEntryDTO GetProductEntryByID<IDType>(IDType id);
        ProductEntryDTO GetProductEntryByIDAsync<IDType>(IDType id);
        ProductEntryDTO GetProductEntryByFilter(Func<ProductEntryDTO, bool> filter);
        IEnumerable<ProductEntryDTO> GetAllProductEntrys();
        IEnumerable<ProductEntryDTO> GetAllProductEntrysByFilter(Func<ProductEntryDTO, bool> filter);

        #endregion

        #region Update
        public void UpdateProductEntry<IDType>(IDType ID, ProductEntryDTO newProductEntry);

        #endregion

        #region Delete
        void DeleteProductEntry(ProductEntryDTO ProductEntryDTO);
        void DeleteProductEntryById(int id);

        void DeleteRangeOfProductEntrys(IEnumerable<ProductEntryDTO> productEntrysDTO);

        #endregion

        #region Additional Functionalities

        bool Exist(int Id);
        ProductEntryDTO GetAllEntriesItems(int productEntryId);

        #endregion

    }
}
