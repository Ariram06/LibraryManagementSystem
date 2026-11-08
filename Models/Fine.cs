namespace LibraryManagementSystem.Models
{
    public class Fine
    {
        public int FineId { get; set; }
        public int BorrowRecordId { get; set; }
        public int FineAmount { get; set; }
        public int PaidAmount { get; set; }
        public string PaymentStatus { get; set; } = "";
        public DateTime? PaidDate { get; set; }
    }
}
