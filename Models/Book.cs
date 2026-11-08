namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = "";

        public string Author { get; set; } = "";
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }

        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
    }
}
