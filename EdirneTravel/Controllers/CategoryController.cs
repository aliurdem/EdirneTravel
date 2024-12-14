using EdirneTravel.Application.Services.Base;
using EdirneTravel.Controllers.Base;
using EdirneTravel.Models.Dtos;
using EdirneTravel.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EdirneTravel.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class CategoryController : BaseController<Category, CategoryDto>
    {
        public CategoryController(IService<Category, CategoryDto> service) : base(service)
        {
        }
    }
}
