using System;
using System.Collections.Generic;

namespace LibraryAPI.Models;

public partial class Book
{
    public int bookId { get; set; }

    public string title { get; set; } = null!;

    public DateTime publishDate { get; set; }

    public int authorId { get; set; }

    public int categoryId { get; set; }

    public virtual Author author { get; set; } = null!;

    public virtual Category category { get; set; } = null!;
}
