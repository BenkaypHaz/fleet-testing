//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using Library.Infraestructure.Common.Helpers;
//using Library.Infraestructure.Common.ResponseHandler;
//using Library.Infraestructure.Persistence.DTOs.General.Bank.Create;
//using Library.Infraestructure.Persistence.DTOs.General.Bank.Read;
//using Library.Infraestructure.Persistence.DTOs.General.Bank.Update;
//using Library.Infraestructure.Persistence.Models.PostgreSQL;
//using Microsoft.EntityFrameworkCore;

//namespace Library.Infraestructure.Persistence.Repositories.General
//{
//    public class BankRepository
//    {
//        private readonly DataBaseContext _context;
//        private readonly IMapper _mapper;
//        public BankRepository(DataBaseContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public async Task<GenericResponseHandler<List<BankReadDto>>> Get()
//        {
//            try
//            {
//                var data = await _context.Banks
//                    .AsNoTracking()
//                    .ProjectTo<BankReadDto>(_mapper.ConfigurationProvider)
//                    .ToListAsync();
//                return new GenericResponseHandler<List<BankReadDto>>(200, data, data.Count());
//            }
//            catch (Exception Ex)
//            {
//                await BaseHelper.SaveErrorLog(Ex);
//                return new GenericResponseHandler<List<BankReadDto>>(500, null, exceptionMessage: Ex.Message);
//            }
//        }

//        public async Task<GenericResponseHandler<BankReadFirstDto>> GetById(long bankId)
//        {
//            try
//            {
//                var data = await _context.Banks
//                    .Where(x => x.Id == bankId)
//                    .AsNoTracking()
//                    .ProjectTo<BankReadFirstDto>(_mapper.ConfigurationProvider)
//                    .FirstOrDefaultAsync();
//                var dataRecords = data != null ? 1 : 0;

//                return new GenericResponseHandler<BankReadFirstDto>(200, data, dataRecords);
//            }
//            catch (Exception Ex)
//            {
//                await BaseHelper.SaveErrorLog(Ex);
//                return new GenericResponseHandler<BankReadFirstDto>(500, null, exceptionMessage: Ex.Message);
//            }
//        }

//        public async Task<GenericResponseHandler<long?>> Create(BankCreateDto payload, long userId)
//        {
//            try
//            {
//                var model = _mapper.Map<Bank>(payload);
//                model.CreatedBy = userId;
//                await _context.Banks.AddAsync(model);
//                await _context.SaveChangesAsync();
//                return new GenericResponseHandler<long?>(201, model.Id);
//            }
//            catch (Exception Ex)
//            {
//                await BaseHelper.SaveErrorLog(Ex);
//                return new GenericResponseHandler<long?>(500, null, exceptionMessage: Ex.Message);
//            }
//        }

//        public async Task<GenericResponseHandler<long?>> Update(long bankId, BankUpdateDto payload, long userId)
//        {
//            try
//            {
//                var model = await _context.Banks.FindAsync(bankId);
//                if (model == null) return new GenericResponseHandler<long?>(404, null);
//                _mapper.Map(payload, model);
//                model.ModifiedBy = userId;
//                await _context.SaveChangesAsync();
//                return new GenericResponseHandler<long?>(200, model.Id);
//            }
//            catch (Exception Ex)
//            {
//                await BaseHelper.SaveErrorLog(Ex);
//                return new GenericResponseHandler<long?>(500, null, exceptionMessage: Ex.Message);
//            }
//        }

//    }
//}
