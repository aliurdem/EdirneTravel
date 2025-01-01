using EdirneTravel.Core.Exception;
using EdirneTravel.Models.Dtos.TravelRoute;
using EdirneTravel.Models.Dtos;
using EdirneTravel.Models.Entities;
using EdirneTravel.Models.Repositories;
using EdirneTravel.Models.Utilities.Results;
using EdirneTravel.Application.Services.Base;
using Microsoft.EntityFrameworkCore;
using Mapster;
using System.Linq;
using EdirneTravel.Models.Entities.Base;
using EdirneTravel.Models.Utilities.Filtering;
using EdirneTravel.Models.Utilities.Paging;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace EdirneTravel.Application.Services
{
    public class TravelRouteService : Service<TravelRoute, TravelRouteDto>, ITravelRouteService
    {
        private readonly IRepository<TravelRoutePlace> _travelRoutePlaceRepository;
        private readonly IRepository<Place> _placeRepository;

        public TravelRouteService(
            IRepository<TravelRoute> repository,
            IRepository<TravelRoutePlace> travelRoutePlaceRepository,
            IRepository<Place> placeRepository)
            : base(repository)
        {
            _travelRoutePlaceRepository = travelRoutePlaceRepository;
            _placeRepository = placeRepository;
        }

        public IDataResult<TravelRoute> SaveTravelRouteWithPlaces(TravelRouteDto travelRouteDto)
        {
            // Transaction başlat
            using var transaction = _repository.BeginTransactionAsync().Result;
            try
            {
                if (travelRouteDto.Places is not null && travelRouteDto.Places.Count > 1)
                {
                    bool checkDuplicatePlace = travelRouteDto.Places
                         .GroupBy(p => p.Id)
                         .Any(g => g.Count() > 1);
                    if (checkDuplicatePlace)
                    {
                        throw new Exception("You cannot add dublicate place values.");
                    }

                    bool checkDuplicateSequence = travelRouteDto.Places
                        .GroupBy(p => p.Sequence)
                        .Any(g => g.Count() > 1);

                    if (checkDuplicateSequence)
                    {
                        throw new Exception("Sequence value must be unique.");
                    }
                }


                if (travelRouteDto == null)
                {
                    throw new ArgumentNullException(nameof(travelRouteDto), "TravelRouteDto cannot be null.");
                }

                TravelRoute travelRoute;

                if (travelRouteDto.Id != 0)
                {
                    // Mevcut TravelRoute güncelle
                    travelRoute = _repository.GetById(travelRouteDto.Id);
                    if (travelRoute == null)
                    {
                        throw new ResourceNotFoundException("ERR_ROUTE_NOT_FOUND");
                    }

                    travelRoute.Name = travelRouteDto.Name;
                    travelRoute.CategoryId = travelRouteDto.CategoryId;
                    travelRoute.ImageData = travelRouteDto.ImageData;

                    _repository.Update(travelRoute);

                    // İlişkili TravelRoutePlace'leri temizle
                    var existingPlaces = _travelRoutePlaceRepository
                        .Select(trp => trp.TravelRouteId == travelRoute.Id)
                        .ToList();

                    _travelRoutePlaceRepository.DeleteRange(existingPlaces);
                }
                else
                {
                    // Yeni TravelRoute oluştur
                    travelRoute = new TravelRoute
                    {
                        Name = travelRouteDto.Name,
                        UserId = travelRouteDto.UserId,
                        CategoryId = travelRouteDto.CategoryId,
                        AverageDuration = travelRouteDto.AverageDuration,
                        ImageData = travelRouteDto.ImageData,
                    };

                    _repository.Insert(travelRoute);
                    _repository.SaveChanges();
                }

                // Yeni yer ilişkilerini ekle
                foreach (var placeDto in travelRouteDto.Places)
                {
                    // Place ID doğrula
                    if (!_placeRepository.Exists(p => p.Id == placeDto.Id))
                    {
                        throw new ResourceNotFoundException($"Place with ID {placeDto.Id} not found.");
                    }

                    var travelRoutePlace = new TravelRoutePlace
                    {
                        TravelRouteId = travelRoute.Id,
                        PlaceId = placeDto.Id,
                        Sequence = placeDto.Sequence
                    };

                    _travelRoutePlaceRepository.Insert(travelRoutePlace);
                }

                _repository.SaveChanges();

                transaction.Commit();

                return new SuccessDataResult<TravelRoute>(travelRoute, "TravelRoute saved successfully with associated places");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new ErrorDataResult<TravelRoute>(ex.Message);
            }
        }

        public IDataResult<TravelRouteDto> GetTravelRouteWithPlacesById(int id)
        {
            var travelRoute = _repository.GetById(id);

            if (travelRoute == null)
                throw new ResourceNotFoundException("ERR_ROUTE_NOT_FOUND");

            var routePlacesQuery = _travelRoutePlaceRepository
                .Select(trp => trp.TravelRouteId == travelRoute.Id)
                .OrderBy(trp => trp.Sequence);

            var routePlacesWithPlace = routePlacesQuery
                .AsQueryable()
                .Include(trp => trp.Place)
                .ToList();

            var places = routePlacesWithPlace.Select(trp => new TravelRoutePlaceDto
            {
                Id = trp.PlaceId,
                Sequence = trp.Sequence,
                Longitude = trp.Place.Longitude,
                Latitude = trp.Place.Latitude,
                Name = trp.Place.Name,
                Description = trp.Place.Description,
            }).ToList();

            var dto = new TravelRouteDto
            {
                Id = travelRoute.Id,
                Name = travelRoute.Name,
                ImageData = travelRoute.ImageData,
                AverageDuration = travelRoute.AverageDuration,
                CategoryId = travelRoute.CategoryId,
                UserId = travelRoute.UserId,
                Places = places
            };
            return new SuccessDataResult<TravelRouteDto>(dto);
        }

        public IDataResult<PagedList<TravelRouteDto>> GetListWithPlaces([FromQuery] PaginationParameters paginationParameters, [FromBody] FilterParameters filterParameters)
        {
            PagedList<TravelRoute> pagedList = _repository.GetList(paginationParameters, filterParameters);

            if (pagedList == null || !pagedList.Any())
            {
                return new ErrorDataResult<PagedList<TravelRouteDto>>("No travel routes found for the specified user and filters.");
            }

            var items = new List<TravelRouteDto>();

            foreach (var item in pagedList)
            {
                var withPlacesDto = GetTravelRouteWithPlacesById(item.Id).Data;
                items.Add(withPlacesDto);
            }

            var pagedListDto = new PagedList<TravelRouteDto>(
                items,
                pagedList.MetaData.TotalCount,
                paginationParameters.PageNumber,
                paginationParameters.PageSize
            );

            return new SuccessDataResult<PagedList<TravelRouteDto>>(pagedListDto);
        }

    }
}
