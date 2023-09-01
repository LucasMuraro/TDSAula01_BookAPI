using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var books = new List<BookAPI.Models.BookModel>
{
    new BookAPI.Models.BookModel 
    { 
        BookId = 1, 
        BookName = "Matthew Desmond", 
        BookTitle="Poverty, By America", 
        BookCurrency = '$',
        BookPrice = 29.99f,
        BookPublicationDate = DateTime.Now
    },
        new BookAPI.Models.BookModel
    {
        BookId = 2,
        BookName = "Dennis Lehane",
        BookTitle="Small Mercies: A Novel",
        BookCurrency = '$',
        BookPrice = 15.55f,
        BookPublicationDate = DateTime.Now
    }
};

builder.Services.AddSingleton(books);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Book.API", Version = "v1" });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/books", () =>
{
    var bookService = app.Services.GetRequiredService<List<BookAPI.Models.BookModel>>();
    return Results.Ok(bookService);
});

app.MapGet("/books/{id}", (int id, HttpRequest request) =>
{
    var booksService = app.Services.GetRequiredService<List<BookAPI.Models.BookModel>>();

    var book = booksService.FirstOrDefault(b => b.BookId == id);

    if(book == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(books);

});

app.MapPost("/books", (BookAPI.Models.BookModel book) =>
{
    var bookServices = app.Services.GetRequiredService<List<BookAPI.Models.BookModel>>();

    book.BookId = bookServices.Max(b => b.BookId) + 1;

    bookServices.Add(book);

    return Results.Created($"/books/{book.BookId}", book);
});

app.MapPut("/books/{id}", (int id, BookAPI.Models.BookModel book) =>
{
    var bookService = app.Services.GetRequiredService<List<BookAPI.Models.BookModel>>();

    var existingBook = bookService.FirstOrDefault(b => b.BookId == id);
    if(existingBook == null)
    {
        return Results.NotFound();
    }

    existingBook.BookTitle = book.BookTitle;
    existingBook.BookName = book.BookName;
    existingBook.BookCurrency = book.BookCurrency;
    existingBook.BookPrice = book.BookPrice;
    existingBook.BookPublicationDate = book.BookPublicationDate;

    return Results.NoContent();

});

app.MapDelete("/books/{id}", (int id) =>
{
    var bookService = app.Services.GetRequiredService<List<BookAPI.Models.BookModel>>();

    var existingBook = bookService.FirstOrDefault(b => b.BookId == id);
    
    if(existingBook == null)
    {
        return Results.NotFound();
    }

    bookService.Remove(existingBook);

    return Results.NoContent();
});

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.Run();
