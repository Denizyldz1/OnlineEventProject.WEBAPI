using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEvent.Abstract.Services;
using OnlineEvent.Core.EntityConst;
using OnlineEvent.Model;
using OnlineEvent.Model.AppUserEventModels;
using OnlineEvent.Model.EventModels;
using OnlineEvent.Model.TicketModels;
using OnlineEvent.Service.Services;
using System.Net.Mime;

namespace OnlineEvent.API.Controllers
{
    public class EventController : CustomBaseController
    {
        private readonly IEventService _eventService;
        private readonly IUserService _userService;

        public EventController(IEventService eventService, IUserService userService)
        {
            _eventService = eventService;
            _userService = userService;
        }
        [Authorize]
        [HttpGet]
        //[Produces("application/xml")]
        public async Task<IActionResult> All()
        {
            var events = await _eventService.GetAllAsync();
            return CreateActionResult(events);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Save(CreateEventModel dto)
        {
            var value = await _eventService.AddAsync(dto);
            return CreateActionResult(value);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var any = _eventService.AnyAsync(x => x.Id == id);
            if (!any.Result) return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(404, $"{id} : numaralı id bulunamadı"));
            var value = await _eventService.GetByIdAsync(id);
            return CreateActionResult(value);

        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateEventModel model)
        {
            // Etkinlik düzenleme yetkisi sadece admin ve etkinlik oluşturucuda olmalı.
            var user = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
            var creatorControl =await _eventService.IsCreatorAsync(user.Data.Id,model.Id);
            if (!creatorControl ||user.Data.UserType != UserTypes.Admin)
            {
                return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(401, "Yetkisiz erişim"));
            }

            var value = await _eventService.GetByIdAsync(model.Id);
            var result = value.Data;
            if (result == null) return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(404, $"{model.Id} : numaralı id bulunamadı"));

            if (result.ApplicationDeadLine.Day + 5 > DateTime.Now.Day) return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(400, "Katılım tarihine 5 gün kalan güncelleme yapılamaz"));

            var eventValue = await _eventService.UpdateAsync(model);
            return CreateActionResult(eventValue);

        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var user = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
            var creatorControl = await _eventService.IsCreatorAsync(user.Data.Id, id);
            if (!creatorControl || user.Data.UserType != UserTypes.Admin)
            {
                return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(401, "Yetkisiz erişim"));
            }

            var value = await _eventService.GetByIdAsync(id);
            var result = value.Data;
            if (result == null) return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(404, $"{id} : numaralı id bulunamadı"));

            if (result.ApplicationDeadLine.Day + 5 > DateTime.Now.Day) return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(400, "Katılım tarihine 5 gün kalan güncelleme yapılamaz"));

            var removeResult = await _eventService.RemoveAsync(id);
            return CreateActionResult(removeResult);

        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("[action]/{eventId}")]
        public async Task<IActionResult> ChangeStatus(int eventId, bool status)
        {
            var eventModel = await _eventService.GetByIdAsync(eventId);
            var result = eventModel.Data;
            if (result == null) return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(400, $"{eventId}:numaralı id bulunamadı"));
            if (result.IsActive == status) return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(400, $"IsActive = {status} mükerrer istek"));
            var value = await _eventService.ChangeStatusAsync(eventId, status);

            return CreateActionResult(value);
        }
        [Authorize]
        [HttpPut("[action]")]
        public async Task<IActionResult> TicketInfo(UpdateTicketInfoModel ticketModel)
        {

            var user = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
            var creatorControl = await _eventService.IsCreatorAsync(user.Data.Id, ticketModel.Id);
                        if (!creatorControl || user.Data.UserType != UserTypes.Admin)

            {
                return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(401, "Yetkisiz erişim"));
            }
            var value = await _eventService.UpdateTicketInfo(ticketModel);
            return CreateActionResult(value);
        }
        [Authorize]
        [HttpPost("[action]/{eventId}")]
        public async Task<IActionResult> JoinEvent(int eventId)
        {
            var quatoControl = await _eventService.JoinedEventListAsync(eventId);
            var quato = await _eventService.GetByIdAsync(eventId);
            if (quatoControl.Data.Count >= quato.Data.Quota)
            {
                return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(400, "Etkinliğe katılma işlemi başarısız kota dolu"));
            }
            if(quato.Data.ApplicationDeadLine < DateTime.Now)
            {
                return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(400, "Etkinliğe katılma işlemi başarısız katılm tarihi geçti"));

            }

            var user = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
            var boolUser = quatoControl.Data.Where(x => x.UserId == user.Data.Id);
            if (boolUser !=null)
            {
                return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(400, "Etkinliğe zaten katılmış görünüyorsunuz"));

            }
            var model = new AppUserEventModel()
            {
                EventId = eventId,
                UserId = user.Data.Id,
                EventUserType = AppUserEventTypes.Participant
            };
            var value = await _eventService.JoinEventAsync(model);
            return CreateActionResult(value);
        }
        [Authorize]
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> AttendedEvents()
        {
            var user = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);

            var value = await _eventService.AttendedEventsAsync(user.Data.Id);
            return CreateActionResult(value);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> EventWithJoinedList()
        {
            var eventList = await _eventService.JoinedEventListAsync();
            return CreateActionResult(eventList);
        }


    }
}
