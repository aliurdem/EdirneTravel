using EdirneTravel.Models.Entities.Base;
using EdirneTravel.Models.Utilities.Filtering;
using EdirneTravel.Models.Utilities.Paging;
using EdirneTravel.Models.Utilities.Results;
using System.Linq.Expressions;

namespace EdirneTravel.Application.Services.Base
{
    public interface IService<TEntity, TDto> where TEntity : BaseEntity where TDto : IDto
    {
        IDataResult<TDto> GetById(int id);
        IDataResult<TDto> Insert(TDto dto);
        IDataResult<TDto> Update(TDto dto);
        void Delete(int id);
        IDataResult<List<TDto>> Select(Expression<Func<TEntity, bool>> filterExpression = null);
        IDataResult<List<TDto>> Search(string keyword);
        IDataResult<List<TDto>> GetAll();
        IDataResult<PagedList<TDto>> GetList(PaginationParameters paginationParameters, FilterParameters filterParameters);
    }
}
