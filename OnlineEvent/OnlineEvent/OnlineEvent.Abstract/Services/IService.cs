using OnlineEvent.Model;
using System.Linq.Expressions;

namespace OnlineEvent.Abstract.Services
{
    public interface IService<TDto, T>
    {
        Task<CustomResponseModel<TDto>> GetByIdAsync(int id);

        Task<CustomResponseModel<IEnumerable<TDto>>> GetAllAsync();

        Task<CustomResponseModel<NoContentModel>> ChangeStatusAsync(int id, bool status);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);



    }
}
