namespace LibraryManagementSystem.DTOs
{
    public class CreateBookDto
    {
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }
    }
}
