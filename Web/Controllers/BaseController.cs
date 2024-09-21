using Microsoft.AspNetCore.Mvc;
using ApplicationLayer.ServicesProvider;
using PersistenceLayer.UnitOfWork.Interfaces;

namespace Web.Controllers
{
	public class BaseController : Controller
	{
		protected IServicePool servicePool;
		public BaseController(IServicePool servicePool)
		{
			this.servicePool = servicePool;
		}
	}
}
