using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ApplicationLayer.Services.ApplicationServices.NotificationServices;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize]
    public class AdminNotificationController : Controller
    {
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        public AdminNotificationController(IHubContext<NotificationHub> notificationHubContext)
        {
            _notificationHubContext = notificationHubContext;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(NotificationMessage model)
        {
            await _notificationHubContext.Clients.All.SendAsync("SendToPOSClerck", model.MessageSubject, model.MessageContent);
            return View();
        }
    }
}
