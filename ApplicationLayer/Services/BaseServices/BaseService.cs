using AutoMapper;
using Microsoft.AspNetCore.Http;
using PersistenceLayer.UnitOfWork.Interfaces;

namespace ApplicationLayer.Services.ApplicationServices.BaseServices
{
	public abstract class BaseService : IBaseService
	{
		public BaseService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}
		public IUnitOfWork unitOfWork { get; }


	}
}
