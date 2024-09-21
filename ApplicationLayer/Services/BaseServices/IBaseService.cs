
using Microsoft.AspNetCore.Http;
using PersistenceLayer.UnitOfWork.Interfaces;


namespace ApplicationLayer.Services.ApplicationServices.BaseServices
{
    public interface IBaseService
    {
        public IUnitOfWork unitOfWork { get; }
    }
}
