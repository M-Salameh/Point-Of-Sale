using ApplicationLayer.Services.ApplicationServices.NotificationServices;
using ApplicationLayer.ServicesProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{

    [Authorize]
    public class NotificationController : BaseController
    {
        private readonly Web.AutoMapper.AutoMapper<NotificationMessage, NotificationViewModel> autoMapper;

        public NotificationController(IServicePool servicePool) : base(servicePool)
        {
            autoMapper = new AutoMapper.AutoMapper<NotificationMessage, NotificationViewModel>();
        }

        public IActionResult Index()
        {
            List<NotificationMessage> notifications = servicePool.NotificationService.GetLatestNotificationMessages(20).ToList();
            List<NotificationViewModel> returnedNotifications = new List<NotificationViewModel>();
            foreach(var notification in notifications)
            {
                returnedNotifications.Add(autoMapper.Map(notification));
            }
            return View(returnedNotifications);
        }
        public IActionResult Add()
        {
            return View("Views/Notification/Add.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> Create(NotificationViewModel notification)
        {
            notification.BroadCastedAt = DateTime.Now;
            NotificationMessage message = autoMapper.Map(notification);

            servicePool.NotificationService.AddNotification(message);

            await servicePool.NotificationService.SendToAllAsync(message);
            TempData["sent"] = "True";
            return View("Views/Notification/Add.cshtml");
        }
    }
}
