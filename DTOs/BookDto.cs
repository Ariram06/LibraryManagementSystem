namespace LibraryManagementSystem.DTOs
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }
    }
}
