using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.Setting.SettingDispatchBranch.Create;
using Library.Infraestructure.Persistence.DTOs.Setting.SettingDispatchBranch.Read;
using Library.Infraestructure.Persistence.DTOs.Setting.SettingDispatchBranch.Update;
using Library.Infraestructure.Persistence.DTOs.Utils.Filters;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Library.Infraestructure.Persistence.Repositories.Setting
{
    public class SettingDispatchBranchRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public SettingDispatchBranchRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenericResponseHandler<List<SettingDispatchBranchReadDto>>> Get(FilterOptionsDto filterOptions)
        {
            var query = _context.SettingDispatchBranches
                .Include(c => c.GeneralCity)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrEmpty(filterOptions.searchValue))
            {
                string[] searchValues = filterOptions.searchValue.Split(" ");
                foreach (var value in searchValues)
                {
                    var lowerValue = value.ToLower();
                    query = query.Where(a =>
                        a.Id.ToString().Equals(value) || a.Id.ToString().Contains(value) ||
                        a.Code.ToString().Equals(value) || a.Code.ToString().Contains(value) ||
                        a.Name.ToLower().Equals(lowerValue) || a.Name.ToLower().Contains(lowerValue) ||
                        a.Address.ToLower().Equals(lowerValue) || a.Address.ToLower().Contains(lowerValue));
                }
            }

            var totalRecords = await query.CountAsync();
            var data = await query
                .Skip((filterOptions.page - 1) * filterOptions.recordsPerPage)
                .Take(filterOptions.recordsPerPage)
                .ProjectTo<SettingDispatchBranchReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new GenericResponseHandler<List<SettingDispatchBranchReadDto>>(200, data, totalRecords);
        }


        public async Task<GenericResponseHandler<SettingDispatchBranchReadFirstDto>> GetById(long id)
        {
                var data = await _context.SettingDispatchBranches
                    .Include(c => c.GeneralCity)
                    .Include(c => c.CreatedByNavigation)
                    .Include(c => c.ModifiedByNavigation)
                    .AsNoTracking()
                    .Where(c => c.Id == id)
                    .ProjectTo<SettingDispatchBranchReadFirstDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                if (data == null)
                    return new GenericResponseHandler<SettingDispatchBranchReadFirstDto>(404, null);

                var dataRecords = data != null ? 1 : 0;
                return new GenericResponseHandler<SettingDispatchBranchReadFirstDto>(200, data, dataRecords);
        }

        public async Task<GenericResponseHandler<long?>> Create(SettingDispatchBranchCreateDto model, long userId)
        {        
                var entity = _mapper.Map<SettingDispatchBranch>(model);
                entity.CreatedBy = userId;
                entity.CreatedDate = DateTime.Now;
                entity.IsActive = true;

                await _context.SettingDispatchBranches.AddAsync(entity);
                await _context.SaveChangesAsync();

                return new GenericResponseHandler<long?>(201, entity.Id);        
        }

        public async Task<GenericResponseHandler<long?>> Update(long id, SettingDispatchBranchUpdateDto model, long userId)
        {
                var entity = await _context.SettingDispatchBranches.FindAsync(id);

                if (entity == null)
                    return new GenericResponseHandler<long?>(404, null);

                _mapper.Map(model, entity);
                entity.ModifiedBy = userId;
                entity.ModifiedDate = DateTime.Now;

                _context.SettingDispatchBranches.Update(entity);
                await _context.SaveChangesAsync();

                return new GenericResponseHandler<long?>(200, entity.Id);
        }

        public async Task<GenericResponseHandler<bool>> Delete(long id, long userId)
        {
                var entity = await _context.SettingDispatchBranches.FindAsync(id);

                if (entity == null)
                    return new GenericResponseHandler<bool>(404, false);

                entity.IsActive = false;
                entity.ModifiedBy = userId;
                entity.ModifiedDate = DateTime.Now;

                _context.SettingDispatchBranches.Update(entity);
                await _context.SaveChangesAsync();

                return new GenericResponseHandler<bool>(200, true);
        }
    }
}