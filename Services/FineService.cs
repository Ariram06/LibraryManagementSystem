using LibraryManagementSystem.Models;
using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Services.IServices;
using LibraryManagementSystem.Repository.IRepository;

namespace LibraryManagementSystem.Services
{
    public class FineService : IFineService
    {
        protected readonly IFineRepository obj;
        protected readonly IBorrowRecordRepository recordObj;

        public FineService(IFineRepository FineRepoObj, IBorrowRecordRepository RecordRepoObj)
        {
            obj = FineRepoObj;
            recordObj = RecordRepoObj;
        }

        public async Task<List<FineDto>> GetAllFinesAsync()
        {
            var fines = await obj.GetAllFinesAsync();
            var dtos = new List<FineDto>();

            foreach (var f in fines)
            {
                dtos.Add(new FineDto
                {
                    FineId = f.FineId,
                    BorrowRecordId = f.BorrowRecordId,
                    FineAmount = f.FineAmount,
                    PaidAmount = f.PaidAmount,
                    PaymentStatus = f.PaymentStatus,
                    PaidDate = f.PaidDate
                });
            }
            return dtos;
        }

        public async Task<FineDto> GetFineByIdAsync(int id)
        {
            var f = await obj.GetFineByIdAsync(id);
            if (f == null)
            {
                return null;
            }

            return new FineDto
            {
                FineId = f.FineId,
                BorrowRecordId = f.BorrowRecordId,
                FineAmount = f.FineAmount,
                PaidAmount = f.PaidAmount,
                PaymentStatus = f.PaymentStatus,
                PaidDate = f.PaidDate
            };
        }

        public async Task<string> CreateFineAsync(CreateFineDto f)
        {
            var record = await recordObj.GetRecordByIdAsync(f.BorrowRecordId);
            if (record == null)
            {
                return "Borrow record not found";
            }

            var existingFine = await obj.GetFineByBorrowRecordIdAsync(f.BorrowRecordId);
            if (existingFine != null)
            {
                return "Fine already exists for this borrow record";
            }

            var domainFine = new Fine
            {
                BorrowRecordId = f.BorrowRecordId,
                FineAmount = f.FineAmount,
                PaidAmount = 0,
                PaymentStatus = "Unpaid",
                PaidDate = null
            };

            await obj.AddFineAsync(domainFine);
            return "fine added successfully";
        }

        public async Task<bool> PayFineAsync(int id, PayFineDto p)
        {
            var fine = await obj.GetFineByIdAsync(id);
            if (fine == null || fine.PaymentStatus == "Paid")
            {
                return false;
            }

            fine.PaidAmount += p.AmountToPay;

            if (fine.PaidAmount >= fine.FineAmount)
            {
                fine.PaymentStatus = "Paid";
                fine.PaidDate = DateTime.UtcNow;
            }
            else
            {
                fine.PaymentStatus = "Partially Paid";
            }

            await obj.UpdateFineAsync(id, fine);
            return true;
        }

        public async Task<bool> DeleteFineAsync(int id)
        {
            var exist = await obj.GetFineByIdAsync(id);
            if (exist == null)
            {
                return false;
            }

            return await obj.DeleteFineAsync(id);
        }
    }
}
