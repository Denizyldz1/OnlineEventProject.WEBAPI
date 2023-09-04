using AutoMapper;
using OnlineEvent.Abstract.Repositories;
using OnlineEvent.Abstract.Services;
using OnlineEvent.Abstract.UnitOfWorks;
using OnlineEvent.Core.Entities;
using OnlineEvent.Model;
using OnlineEvent.Model.CategoryModels;
using OnlineEvent.Model.EventModels;
using OnlineEvent.Service.Mapping;

namespace OnlineEvent.Service.Services
{
    public class CategoryService : Service<CategoryModel, Category>, ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _repository = repository;
        }

        public async Task<CustomResponseModel<CategoryModel>> AddAsync(CreateCategoryModel Model)
        {
            var categoryNameControl = _repository.Where(x => x.CategoryName == Model.CategoryName).FirstOrDefault();
            if (categoryNameControl != null)
            {
                return CustomResponseModel<CategoryModel>.Failure(400, $"{Model.CategoryName} adı zaten kayıt edilmiş.");

            }
            var entity = _mapper.Map<Category>(Model);
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            var categoryDto = _mapper.Map<CategoryModel>(entity);
            return CustomResponseModel<CategoryModel>.Success(201, categoryDto);
        }

        public async Task<CustomResponseModel<CategoryWithEventModel>> GetByIdWithEventsAsync(int categoryId)
        {
            var category = await _repository.GetByIdWithEventsAsync(categoryId);
            var eventList = category.Events.ToList();
            EventMapping eventMapping = new EventMapping();
            var dtoEntities = new List<EventModel>();
            eventList.ForEach(e => { dtoEntities.Add(eventMapping.EventMap(e)); });
            var mapping = new CategoryWithEventModel()
            {
                Id = categoryId,
                CategoryName = category.CategoryName,
                IsActive = category.IsActive
            };
            mapping.Events = dtoEntities;
            return CustomResponseModel<CategoryWithEventModel>.Success(200, mapping);
        }

        public async Task<CustomResponseModel<NoContentModel>> RemoveAsync(int id)
        {
            var value = await _repository.GetByIdAsync(id);
            _repository.Remove(value);
            await _unitOfWork.CommitAsync();
            return CustomResponseModel<NoContentModel>.Success(204);
        }

        public async Task<CustomResponseModel<NoContentModel>> UpdateAsync(UpdateCategoryModel Model)
        {
            var categoryNameControl = _repository.Where(x => x.CategoryName == Model.CategoryName).FirstOrDefault();
            if (categoryNameControl != null)
            {
                return CustomResponseModel<NoContentModel>.Failure(400, $"{Model.CategoryName} adı zaten kayıt edilmiş.");

            }
            var value = _mapper.Map<Category>(Model);
            _repository.Update(value);
            await _unitOfWork.CommitAsync();
            return CustomResponseModel<NoContentModel>.Success(204);
        }

        protected override CategoryModel MapToDto(Category entity)
        {
            return _mapper.Map<CategoryModel>(entity);

        }
    }
}
