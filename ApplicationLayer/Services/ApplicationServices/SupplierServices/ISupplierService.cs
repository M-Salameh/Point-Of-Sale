using System;
using ApplicationLayer.DTOs;
using System.Collections.Generic;
using ApplicationLayer.Services.ApplicationServices.BaseServices;

namespace ApplicationLayer.Services.ApplicationServices.SupplierServices
{
    public interface ISupplierService : IBaseService
    {

        #region Create

        void AddSupplierAsync(SupplierDTO supplierDTO);
        void AddSupplier(SupplierDTO supplierDTO);
        void AddRangeOfSuppliers(IEnumerable<SupplierDTO> suppliersDTO);
        void AddRangeOfSuppliersAsync(IEnumerable<SupplierDTO> suppliersDTO);

        #endregion

        #region Read

        SupplierDTO GetSupplierByID<IDType>(IDType id);
        SupplierDTO GetSupplierByIDAsync<IDType>(IDType id);
        SupplierDTO GetSupplierByFilter(Func<SupplierDTO, bool> filter);
        IEnumerable<SupplierDTO> GetAllSuppliers();
        IEnumerable<SupplierDTO> GetAllSuppliersByFilter(Func<SupplierDTO, bool> filter);

        #endregion

        #region Update
        public void UpdateSupplier<IDType>(IDType ID, SupplierDTO newSupplier);

        #endregion

        #region Delete

        void DeleteSupplier(SupplierDTO supplierDTO);
        void DeleteRangeOfSuppliers(IEnumerable<SupplierDTO> suppliersDTO);
        void DeleteSupplierById(int id);

        #endregion

        #region Additional Functionalities

        public void AddProducts(int supplierId, int productId, int quantity, int price);
        bool Exist(int Id);
        int AddSupplierAndGetId(SupplierDTO supplierDTO);
        SupplierDTO GetAllProductEntries(int supplierId);

        #endregion



    }
}
