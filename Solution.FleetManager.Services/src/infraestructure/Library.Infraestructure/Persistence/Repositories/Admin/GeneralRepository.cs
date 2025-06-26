using Library.Infraestructure.Common.Helpers;
using Library.Infraestructure.Common.ResponseHandler;
using Library.Infraestructure.Persistence.DTOs.General.Utils;
using Library.Infraestructure.Persistence.Models;
using Library.Infraestructure.Persistence.Models.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infraestructure.Persistence.Repositories.Admin
{
    public class GeneralRepository
    {
        private readonly DataBaseContext _context;
        public GeneralRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<GenericHandlerResponse<List<GenericDropDown>>> GetStates()
        {
            try
            {
                var data = await _context.GeneralRegions
                    .AsNoTracking()
                    .Select(x => new GenericDropDown
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }).ToListAsync();
                return new GenericHandlerResponse<List<GenericDropDown>>(200, data, data.Count());
            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericHandlerResponse<List<GenericDropDown>>(500, ExceptionMessage: Ex.Message);
            }
        }

        public async Task<GenericHandlerResponse<List<GenericDropDown>>> GetCities(long regionId)
        {
            try
            {
                var data = await _context.GeneralCities
                    .AsNoTracking()
                    .Where(x => x.RegionId == regionId)
                    .Select(x => new GenericDropDown
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }).ToListAsync();
                return new GenericHandlerResponse<List<GenericDropDown>>(200, data, data.Count());
            }
            catch (Exception Ex)
            {
                await BaseHelper.SaveErrorLog(Ex);
                return new GenericHandlerResponse<List<GenericDropDown>>(500, ExceptionMessage: Ex.Message);
            }
        }

    }
}
