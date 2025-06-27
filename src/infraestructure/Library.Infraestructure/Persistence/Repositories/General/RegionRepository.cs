using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.General.Region.Read;
using Library.Infraestructure.Persistence.DTOs.General.Country.Read;
using Microsoft.EntityFrameworkCore;
using Library.Infraestructure.Persistence.DTOs.General.Region.Create;
using Library.Infraestructure.Persistence.DTOs.General.Region.Update;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.ComponentModel.Design;

namespace Library.Infraestructure.Persistence.Repositories.General
{
    public class RegionRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public RegionRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponseHandler<List<RegionReadDto>>> Get()
        {
            try
            {
                var data = await _context.GeneralRegions
                    .Include(c => c.Country)
                    .AsNoTracking()
                    .ProjectTo<RegionReadDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return new GenericResponseHandler<List<RegionReadDto>>(200, data, data.Count());
            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericResponseHandler<List<RegionReadDto>>(500, null, exceptionMessage: Ex.Message);
            }
        }

        public async Task<GenericResponseHandler<RegionReadFirstDto>> GetById(long regionId)
        {
            try
            {
                var data = await _context.GeneralRegions
                    .Include(c => c.Country)
                    .AsNoTracking()
                    .Where(x => x.Id == regionId)
                    .ProjectTo<RegionReadFirstDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
                var dataRecords = data != null ? 1 : 0;

                return new GenericResponseHandler<RegionReadFirstDto>(200, data, dataRecords);
            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericResponseHandler<RegionReadFirstDto>(500, null, exceptionMessage: Ex.Message);
            }
        }

        public async Task<GenericResponseHandler<long?>> Create(RegionCreateDto payload, long userId)
        {
            try
            {
                var model = _mapper.Map<GeneralRegion>(payload);
                model.CreatedBy = userId;
                await _context.GeneralRegions.AddAsync(model);
                await _context.SaveChangesAsync();
                return new GenericResponseHandler<long?>(201, model.Id);
            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericResponseHandler<long?>(500, null, exceptionMessage: Ex.Message);
            }
        }

        public async Task<GenericResponseHandler<long?>> Update(long regionId, RegionUpdateDto payload, long userId)
        {
            try
            {
                var model = await _context.GeneralRegions.FindAsync(regionId);
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
