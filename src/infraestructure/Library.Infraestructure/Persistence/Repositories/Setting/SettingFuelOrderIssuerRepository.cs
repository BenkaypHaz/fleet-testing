using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Setting.FuelOrderIssuer.Create;
using Library.Infraestructure.Persistence.DTOs.Setting.FuelOrderIssuer.Read;
using Library.Infraestructure.Persistence.DTOs.Setting.FuelOrderIssuer.Update;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Library.Infraestructure.Persistence.Repositories.Setting
{
    public class SettingFuelOrderIssuerRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public SettingFuelOrderIssuerRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponseHandler<List<SettingFuelOrderIssuerReadDto>>> Get(FilterOptionsDto filterOptions)
        {
                var query = _context.SettingFuelOrderIssuers
                    .AsNoTracking()
                    .Where(c => c.IsActive == true)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(filterOptions.searchValue))
                {
                    string[] searchValues = filterOptions.searchValue.Split(" ");
                    foreach (var value in searchValues)
                    {
                        query = query.Where(a =>
                            a.Id.ToString().Equals(value) || a.Id.ToString().Contains(value) ||
                            a.Name.ToLower().Equals(value) || a.Name.ToLower().Contains(value));
                    }
                }

                var totalRecords = await query.CountAsync();
                var data = await query
                    .Skip((filterOptions.page - 1) * filterOptions.recordsPerPage)
                    .Take(filterOptions.recordsPerPage)
                    .ProjectTo<SettingFuelOrderIssuerReadDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return new GenericResponseHandler<List<SettingFuelOrderIssuerReadDto>>(200, data, totalRecords);
        }

        public async Task<GenericResponseHandler<SettingFuelOrderIssuerReadFirstDto>> GetById(long id)
        {
                var data = await _context.SettingFuelOrderIssuers
                    .Include(c => c.CreatedByNavigation)
                    .Include(c => c.ModifiedByNavigation)
                    .AsNoTracking()
                    .Where(c => c.Id == id)
                    .ProjectTo<SettingFuelOrderIssuerReadFirstDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                if (data == null)
                    return new GenericResponseHandler<SettingFuelOrderIssuerReadFirstDto>(404, null);

                var dataRecords = data != null ? 1 : 0;
                return new GenericResponseHandler<SettingFuelOrderIssuerReadFirstDto>(200, data, dataRecords);
        }

        public async Task<GenericResponseHandler<long?>> Create(SettingFuelOrderIssuerCreateDto model, long userId)
        { 
                if (model.IsDefault)
                {
                    var existingDefaults = await _context.SettingFuelOrderIssuers
                        .Where(x => x.IsDefault && x.IsActive)
                        .ToListAsync();

                    foreach (var item in existingDefaults)
                    {
                        item.IsDefault = false;
                    }
                }

                var entity = _mapper.Map<SettingFuelOrderIssuer>(model);
                entity.CreatedBy = userId;
                entity.CreatedDate = DateTime.Now;
                entity.IsActive = true;

                await _context.SettingFuelOrderIssuers.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new GenericResponseHandler<long?>(201, entity.Id);
        }

        public async Task<GenericResponseHandler<long?>> Update(long id, SettingFuelOrderIssuerUpdateDto model, long userId)
        {
                var entity = await _context.SettingFuelOrderIssuers.FindAsync(id);

                if (entity == null)
                    return new GenericResponseHandler<long?>(404, null);

                if (model.IsDefault && !entity.IsDefault)
                {
                    var existingDefaults = await _context.SettingFuelOrderIssuers
                        .Where(x => x.IsDefault && x.IsActive && x.Id != id)
                        .ToListAsync();

                    foreach (var item in existingDefaults)
                    {
                        item.IsDefault = false;
                    }
                }

                _mapper.Map(model, entity);
                entity.ModifiedBy = userId;
                entity.ModifiedDate = DateTime.Now;

                _context.SettingFuelOrderIssuers.Update(entity);
                await _context.SaveChangesAsync();

                return new GenericResponseHandler<long?>(200, entity.Id);
           
        }
    }
}