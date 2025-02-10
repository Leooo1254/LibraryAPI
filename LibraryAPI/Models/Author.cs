using System;
using System.Collections.Generic;

namespace LibraryAPI.Models;

public partial class Author
{
    public int authorId { get; set; }

    public string authorName { get; set; } = null!;

    public virtual ICollection<Book> books { get; set; } = new List<Book>();
}
