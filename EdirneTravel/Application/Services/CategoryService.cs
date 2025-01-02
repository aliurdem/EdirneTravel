using EdirneTravel.Application.Services.Base;
using EdirneTravel.Models.Dtos;
using EdirneTravel.Models.Entities;
using EdirneTravel.Models.Repositories;
using EdirneTravel.Models.Utilities.Filtering;
using EdirneTravel.Models.Utilities.Paging;
using System.Security.Claims;

namespace EdirneTravel.Application.Services
{
    public class CategoryService : Service<Category, CategoryDto>, ICategoryService
    {
        private readonly IRepository<Place> _placeRepository;

        public CategoryService(IRepository<Category> repository, IRepository<Place> placeRepository) : base(repository)
        {
            _placeRepository = placeRepository;
        }

        public override void Delete(int id)
        {
           

            base.Delete(id);
        }
    }
}
