using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineEvent.Abstract.Repositories;
using OnlineEvent.Abstract.Services;
using OnlineEvent.Abstract.UnitOfWorks;
using OnlineEvent.Core;
using OnlineEvent.Model;
using System.Linq.Expressions;

namespace OnlineEvent.Service.Services
{
    public abstract class Service<TDto, T> : IService<TDto, T> where T : class, IEntity where TDto : class, new()
    {
        private readonly IGenericRepository<T> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        protected abstract TDto MapToDto(T entity);
        public async Task<CustomResponseModel<IEnumerable<TDto>>> GetAllAsync()
        {
            var entities = await _repository.GetAll().ToListAsync();
            var dtoEntities = new List<TDto>();
            entities.ForEach(entity => { dtoEntities.Add(MapToDto(entity)); });
            return CustomResponseModel<IEnumerable<TDto>>.Success(200, dtoEntities);
        }

        public async Task<CustomResponseModel<TDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var dtoEntity = MapToDto(entity);
            return CustomResponseModel<TDto>.Success(200, dtoEntity);
        }

        public async Task<CustomResponseModel<NoContentModel>> ChangeStatusAsync(int id, bool status)
        {
            await _repository.ChangeStatusAsync(id, status);
            await _unitOfWork.CommitAsync();
            return CustomResponseModel<NoContentModel>.Success(204);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

    }
}
