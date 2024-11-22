namespace Library.Domain
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }  // Строка, содержащая имя автора
        public DateTime? BorrowedAt { get; set; }
    }
}
