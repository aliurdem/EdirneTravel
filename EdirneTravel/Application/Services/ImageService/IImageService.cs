using EdirneTravel.Models.Utilities.Results;

namespace EdirneTravel.Application.Services.ImageService
{
    public interface IImageService
    {
        public IDataResult<byte[]> GetImage(int productId);
    }
}
