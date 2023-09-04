using OnlineEvent.Core.Entities;
using OnlineEvent.Model;
using OnlineEvent.Model.CategoryModels;

namespace OnlineEvent.Abstract.Services
{
    public interface ICategoryService : IService<CategoryModel, Category>
    {
        Task<CustomResponseModel<CategoryModel>> AddAsync(CreateCategoryModel Model);
        Task<CustomResponseModel<NoContentModel>> UpdateAsync(UpdateCategoryModel Model);
        Task<CustomResponseModel<NoContentModel>> RemoveAsync(int id);
        Task<CustomResponseModel<CategoryWithEventModel>> GetByIdWithEventsAsync(int categoryId);
    }
}
