using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.General.Country.Create;
using Library.Infraestructure.Persistence.DTOs.General.Country.Read;
using Library.Infraestructure.Persistence.DTOs.General.Country.Update;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.Repositories.General
{
    public class CountryRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;
        public CountryRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponseHandler<List<CountryReadDto>>> Get()
        {
            try
            {
                var data = await _context.GeneralCountries
                    .AsNoTracking()
                    .ProjectTo<CountryReadDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return new GenericResponseHandler<List<CountryReadDto>>(200, data, data.Count());
            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericResponseHandler<List<CountryReadDto>>(500, null, exceptionMessage: Ex.Message);
            }
        }

        public async Task<GenericResponseHandler<CountryReadFirstDto>> GetById(long countryId)
        {
            try
            {
                var data = await _context.GeneralCountries
                    .AsNoTracking()
                    .Where(x => x.Id == countryId)
                    .ProjectTo<CountryReadFirstDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
                var dataRecords = data != null ? 1 : 0;

                return new GenericResponseHandler<CountryReadFirstDto>(200, data, dataRecords);
            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericResponseHandler<CountryReadFirstDto>(500, null, exceptionMessage: Ex.Message);
            }
        }

        public async Task<GenericResponseHandler<long?>> Create(CountryCreateDto payload, long userId)
        {
            try
            {
                var model = _mapper.Map<GeneralCountry>(payload);
                model.CreatedBy = userId;
                await _context.GeneralCountries.AddAsync(model);
                await _context.SaveChangesAsync();
                return new GenericResponseHandler<long?>(201, model.Id);
            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericResponseHandler<long?>(500, null, exceptionMessage: Ex.Message);
            }
        }

        public async Task<GenericResponseHandler<long?>> Update(long countryId, CountryUpdateDto payload, long userId)
        {
            try
            {
                var model = await _context.GeneralCountries.FindAsync(countryId);
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
