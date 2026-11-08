namespace LibraryManagementSystem.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";

        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
    }
}
