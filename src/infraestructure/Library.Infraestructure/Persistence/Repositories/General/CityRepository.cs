using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.General.City.Create;
using Library.Infraestructure.Persistence.DTOs.General.City.Read;
using Library.Infraestructure.Persistence.DTOs.General.City.Update;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Library.Infraestructure.Persistence.Repositories.General
{
    public class CityRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;
        public CityRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponseHandler<List<CityReadDto>>> Get()
        {
            try
            {
                var data = await _context.GeneralCities
                    .AsNoTracking()
                    .ProjectTo<CityReadDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return new GenericResponseHandler<List<CityReadDto>>(200, data, data.Count());
            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericResponseHandler<List<CityReadDto>>(500, null, exceptionMessage: Ex.Message);
            }
        }

        public async Task<GenericResponseHandler<CityReadFirstDto>> GetById(long cityId)
        {
            try
            {
                var data = await _context.GeneralCities
                    .AsNoTracking()
                    .Where(x => x.Id == cityId)
                    .ProjectTo<CityReadFirstDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
                var dataRecords = data != null ? 1 : 0;

                return new GenericResponseHandler<CityReadFirstDto>(200, data, dataRecords);
            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericResponseHandler<CityReadFirstDto>(500, null, exceptionMessage: Ex.Message);
            }
        }

        public async Task<GenericResponseHandler<List<CityReadDto>>> GetByRegionId(long regionId)
        {
            try
            {
                var data = await _context.GeneralCities
                    .AsNoTracking()
                    .Where(x => x.RegionId == regionId)
                    .ProjectTo<CityReadDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return new GenericResponseHandler<List<CityReadDto>>(200, data, data.Count());
            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericResponseHandler<List<CityReadDto>>(500, null, exceptionMessage: Ex.Message);
            }
        }

        public async Task<GenericResponseHandler<long?>> Create(CityCreateDto payload, long userId)
        {
            try
            {
                var model = _mapper.Map<GeneralCity>(payload);
                model.CreatedBy = userId;
                await _context.GeneralCities.AddAsync(model);
                await _context.SaveChangesAsync();
                return new GenericResponseHandler<long?>(201, model.Id);
            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericResponseHandler<long?>(500, null, exceptionMessage: Ex.Message);
            }
        }

        public async Task<GenericResponseHandler<long?>> Update(long cityId, CityUpdateDto payload, long userId)
        {
            try
            {
                var model = await _context.GeneralCities.FindAsync(cityId);
                if (model == null) return new GenericResponseHandler<long?>(404, null);
                _mapper.Map(payload, model);
                model.ModifiedBy = userId;
                await _context.SaveChangesAsync();
                return new GenericResponseHandler<long?>(200, model.Id);
            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericResponseHandler<long?>(500, null, exceptionMessage: Ex.Message);
            }
        }

    }
}
