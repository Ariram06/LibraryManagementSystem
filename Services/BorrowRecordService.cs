using LibraryManagementSystem.Models;
using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Services.IServices;
using LibraryManagementSystem.Repository.IRepository;

namespace LibraryManagementSystem.Services
{
    public class BorrowRecordService : IBorrowRecordService
    {
        protected readonly IBorrowRecordRepository obj;
        protected readonly IBookRepository bookObj;
        protected readonly IMemberRepository memberObj;

        public BorrowRecordService(IBorrowRecordRepository RecordRepoObj, IBookRepository BookRepoObj, IMemberRepository MemberRepoObj)
        {
            obj = RecordRepoObj;
            bookObj = BookRepoObj;
            memberObj = MemberRepoObj;
        }

        public async Task<List<BorrowRecordDto>> GetAllRecordsAsync()
        {
            var records = await obj.GetAllRecordsAsync();
            var dtos = new List<BorrowRecordDto>();

            foreach (var br in records)
            {
                dtos.Add(new BorrowRecordDto
                {
                    BorrowRecordId = br.BorrowRecordId,
                    BookId = br.BookId,
                    MemberId = br.MemberId,
                    BorrowDate = br.BorrowDate,
                    DueDate = br.DueDate,
                    ReturnDate = br.ReturnDate,
                    Status = br.Status
                });
            }
            return dtos;
        }

        public async Task<BorrowRecordDto> GetRecordByIdAsync(int id)
        {
            var br = await obj.GetRecordByIdAsync(id);
            if (br == null)
            {
                return null;
            }

            return new BorrowRecordDto
            {
                BorrowRecordId = br.BorrowRecordId,
                BookId = br.BookId,
                MemberId = br.MemberId,
                BorrowDate = br.BorrowDate,
                DueDate = br.DueDate,
                ReturnDate = br.ReturnDate,
                Status = br.Status
            };
        }

        public async Task<string> CreateBorrowRecordAsync(CreateBorrowRecordDto br)
        {
            var targetBook = await bookObj.GetBookByIdAsync(br.BookId);
            var targetMember = await memberObj.GetMemberByIdAsync(br.MemberId);

            if (targetBook == null) return "Book not found";
            if (targetMember == null) return "Member not found";
            if (targetBook.AvailableCopies <= 0) return "No available copies of this book";

            var domainRecord = new BorrowRecord
            {
                BookId = br.BookId,
                MemberId = br.MemberId,
                BorrowDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(14), // Sets standard 14 days layout limit
                ReturnDate = null,
                Status = "Borrowed"
            };

            targetBook.AvailableCopies--;
            await bookObj.UpdateBookAsync(br.BookId, targetBook);
            await obj.AddRecordAsync(domainRecord);

            return "book added successfully";
        }

        public async Task<bool> ReturnBookAsync(int id)
        {
            var existingRecord = await obj.GetRecordByIdAsync(id);
            if (existingRecord == null || existingRecord.ReturnDate != null)
            {
                return false;
            }

            existingRecord.ReturnDate = DateTime.UtcNow;
            existingRecord.Status = "Returned";

            var targetBook = await bookObj.GetBookByIdAsync(existingRecord.BookId);
            if (targetBook != null)
            {
                targetBook.AvailableCopies++;
                await bookObj.UpdateBookAsync(existingRecord.BookId, targetBook);
            }

            await obj.UpdateRecordAsync(id, existingRecord);
            return true;
        }

        public async Task<bool> DeleteRecordAsync(int id)
        {
            var exist = await obj.GetRecordByIdAsync(id);
            if (exist == null)
            {
                return false;
            }

            return await obj.DeleteRecordAsync(id);
        }
    }
}
