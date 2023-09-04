using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEvent.Abstract.Services;
using OnlineEvent.Model;
using OnlineEvent.Model.CategoryModels;
using System.Data;

namespace OnlineEvent.API.Controllers
{
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [Authorize]
        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetByIdWithEvents(int categoryId)
        {
            var any = await _categoryService.AnyAsync(x => x.Id == categoryId);
            if (!any) return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(404, $"{categoryId} : numaralı id bulunamadı"));

            return CreateActionResult(await _categoryService.GetByIdWithEventsAsync(categoryId));
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var categories = await _categoryService.GetAllAsync();
            return CreateActionResult(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var any = await _categoryService.AnyAsync(x => x.Id == id);
            if (!any) return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(404, $"{id} bulunamadı"));
            var category = await _categoryService.GetByIdAsync(id);
            return CreateActionResult(category);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Save(CreateCategoryModel model)
        {

            var value = await _categoryService.AddAsync(model);
            return CreateActionResult(value);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryModel model)
        {
            var any = await _categoryService.AnyAsync(x => x.Id == model.Id);
            if (!any) return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(404, $"{model.Id} bulunamadı"));

            var category = await _categoryService.UpdateAsync(model);
            return CreateActionResult(category);


        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var any = await _categoryService.AnyAsync(x => x.Id == id);
            if (!any) return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(400, $"{id} bulunamadı"));
            var value = await _categoryService.RemoveAsync(id);
            return CreateActionResult(value);

        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("[action]/{categoryId}")]
        public async Task<IActionResult> ChangeStatus(int categoryId, bool status)
        {
            var category = await _categoryService.GetByIdAsync(categoryId);
            var result = category.Data;
            if (result == null) return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(400, $"{categoryId}:numaralı id bulunamadı"));
            if(result.IsActive==status) return CreateActionResult(CustomResponseModel<NoContentModel>.Failure(400, $"IsActive = {status} mükerrer istek"));
            var value = await _categoryService.ChangeStatusAsync(categoryId, status);

            return CreateActionResult(value);
        }

    }
}
