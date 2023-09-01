namespace BookAPI.Models
{
    public class BookModel
    {
        public int BookId { get; set; }
        public string? BookTitle { get; set; }
        public string? BookName { get; set; }
        public char? BookCurrency { get; set; }
        public float? BookPrice { get; set; }
        public DateTime? BookPublicationDate { get; set; }

        /*public ModelBook (int bookId, string? bookTitle, string? bookName, float? bookPrice, DateTime? bookPublicationDate)
        {
            BookId = bookId;
            BookTitle = bookTitle;
            BookName = bookName;
            BookPrice = bookPrice;
            BookPublicationDate = bookPublicationDate;
        }*/
    }
}
