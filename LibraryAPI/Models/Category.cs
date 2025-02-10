using System;
using System.Collections.Generic;

namespace LibraryAPI.Models;

public partial class Category
{
    public int categoryId { get; set; }

    public string categoryName { get; set; } = null!;

    public virtual ICollection<Book> books { get; set; } = new List<Book>();
}
