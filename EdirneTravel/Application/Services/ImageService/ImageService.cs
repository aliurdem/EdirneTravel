using EdirneTravel.Models.Entities;
using EdirneTravel.Models.Repositories;
using EdirneTravel.Models.Utilities.Results;
using Microsoft.VisualBasic;
using System.Net;
using System.Text;

namespace EdirneTravel.Application.Services.ImageService
{
    public class ImageService : IImageService
    {
        private readonly IRepository<Place> _placeRepository;

        public ImageService(IRepository<Place> placeRepository)
        {
            _placeRepository = placeRepository;
        }
        
        public IDataResult<byte[]> GetImage(int productId)
        {
            try
            {
                byte[] imageData = _placeRepository.GetById(productId)?.ImageData;

                if (imageData is not null && imageData.Length > 0)
                {
                    return new SuccessDataResult<byte[]>(imageData, "Success");
                }
                else
                {
                    return new ErrorDataResult<byte[]>("Image Not Found");
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<byte[]>(ex.Message);
            }

        }
    }
}
