using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEvent.Abstract.Services;
using OnlineEvent.Core.Entities;
using OnlineEvent.Model;
using OnlineEvent.Service.Services;

namespace OnlineEvent.API.Controllers
{
    public class CityController : CustomBaseController
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var cities = await _cityService.GetAllAsync();
            return CreateActionResult(cities);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var any = await _cityService.AnyAsync(x => x.Id == id);
            if (!any) return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(404, $"{id} numaralı id bulunamadı"));
           
            var city = await _cityService.GetByIdAsync(id);
            return CreateActionResult(city);
        }
        [Authorize]
        [HttpGet("[action]/{cityId}")]
        public async Task<IActionResult> GetByIdWithEvents(int cityId)
        {
            var any = await _cityService.AnyAsync(x => x.Id == cityId);
            if (!any) return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(404, $"{cityId} numaralı id bulunamadı"));
            return CreateActionResult(await _cityService.GetByIdWithEventAsync(cityId));
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("[action]/{cityId}")]
        public async Task<IActionResult> ChangeStatus(int cityId, bool status)
        {
            var category = _cityService.GetByIdAsync(cityId);
            var result = category.Result.Data;
            if (result == null) return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(400, $"{cityId}:numaralı id bulunamadı"));
            if (result.IsActive == status) return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(400, $"IsActive = {status} mükerrer istek"));
            var value = await _cityService.ChangeStatusAsync(cityId, status);

            return CreateActionResult(value);
        }
    }
}
