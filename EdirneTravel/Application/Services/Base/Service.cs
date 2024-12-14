using EdirneTravel.Core.Exception;
using EdirneTravel.Models.Entities.Base;
using EdirneTravel.Models.Repositories;
using EdirneTravel.Models.Utilities.Filtering;
using EdirneTravel.Models.Utilities.Paging;
using EdirneTravel.Models.Utilities.Results;
using Mapster;
using System.Linq.Expressions;

namespace EdirneTravel.Application.Services.Base
{
    public class Service<TEntity, TDto> : IService<TEntity, TDto> where TEntity : BaseEntity where TDto : IDto
    {
        protected readonly IRepository<TEntity> _repository;

        public Service(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual void Delete(int id)
        {
            TEntity model = _repository.GetById(id);

            if (model == null) { throw new ResourceNotFoundException("ERR_ENTITY_NOT_FOUND"); }

            _repository.Delete(model);
            _repository.SaveChanges();
        }

        public virtual IDataResult<TDto> GetById(int id)
        {
            TEntity model = _repository.GetById(id);

            if (model == null) { throw new ResourceNotFoundException("ERR_ENTITY_NOT_FOUND"); }

            return new SuccessDataResult<TDto>(model.Adapt<TDto>());
        }

        public virtual IDataResult<List<TDto>> GetAll()
        {
            return Select();
        }

        public virtual IDataResult<TDto> Insert(TDto dto)
        {
            TEntity model = dto.Adapt<TEntity>();

            _repository.Insert(model);
            _repository.SaveChanges();

            return new SuccessDataResult<TDto>(model.Adapt<TDto>());
        }

        public virtual IDataResult<List<TDto>> Select(Expression<Func<TEntity, bool>> filterExpression = null)
        {
            List<TEntity> entities = _repository.Select(filterExpression).ToList();
            return new SuccessDataResult<List<TDto>>(entities.Adapt<List<TDto>>());
        }

        public virtual IDataResult<List<TDto>> Search(string keyword)
        {
            throw new NotImplementedException();
        }

        public virtual IDataResult<TDto> Update(TDto dto)
        {
            TEntity model = _repository.GetById(dto.Id);

            if (model == null) { throw new ResourceNotFoundException("ERR_ENTITY_NOT_FOUND"); }

            model = dto.Adapt<TEntity>();

            _repository.Update(model);
            _repository.SaveChanges();

            return new SuccessDataResult<TDto>(model.Adapt<TDto>());
        }

        public virtual IDataResult<PagedList<TDto>> GetList(PaginationParameters paginationParameters, FilterParameters filterParameters)
        {
            PagedList<TEntity> pagedList = _repository.GetList(paginationParameters, filterParameters);

            var items = pagedList.Select(p => p.Adapt<TDto>()).ToList();

            var pagedListDto = new PagedList<TDto>(items, pagedList.MetaData.TotalCount, paginationParameters.PageNumber, paginationParameters.PageSize);

            return new SuccessDataResult<PagedList<TDto>>(pagedListDto);
        }
    }
}
