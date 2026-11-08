using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Repository.IRepository;

namespace LibraryManagementSystem.Repository
{
    public class FineRepository : IFineRepository
    {
        protected readonly AppDbContext obj;

        public FineRepository(AppDbContext AppDBObj)
        {
            obj = AppDBObj;
        }

        public async Task<List<Fine>> GetAllFinesAsync()
        {
            return await obj.Fines.ToListAsync();
        }

        public async Task<Fine> GetFineByIdAsync(int id)
        {
            return await obj.Fines.FindAsync(id);
        }

        public async Task<Fine> GetFineByBorrowRecordIdAsync(int borrowRecordId)
        {
            return await obj.Fines.FirstOrDefaultAsync(f => f.BorrowRecordId == borrowRecordId);
        }

        public async Task AddFineAsync(Fine f)
        {
            await obj.Fines.AddAsync(f);
            await obj.SaveChangesAsync();
        }

        public async Task UpdateFineAsync(int id, Fine f)
        {
            var existingFine = await obj.Fines.FindAsync(id);
            if (existingFine != null)
            {
                existingFine.BorrowRecordId = f.BorrowRecordId;
                existingFine.FineAmount = f.FineAmount;
                existingFine.PaidAmount = f.PaidAmount;
                existingFine.PaymentStatus = f.PaymentStatus;
                existingFine.PaidDate = f.PaidDate;
                await obj.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteFineAsync(int id)
        {
            var f = await obj.Fines.FindAsync(id);
            if (f != null)
            {
                obj.Fines.Remove(f);
                await obj.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
