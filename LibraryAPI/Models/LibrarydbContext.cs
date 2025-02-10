using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Models;

public partial class LibrarydbContext : DbContext
{
    public LibrarydbContext()
    {
    }

    public LibrarydbContext(DbContextOptions<LibrarydbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=librarydb;user=root;password=;sslmode=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.authorId).HasName("PRIMARY");

            entity.ToTable("authors");

            entity.Property(e => e.authorId)
                .HasColumnType("int(11)")
                .HasColumnName("author_id");
            entity.Property(e => e.authorName)
                .HasMaxLength(100)
                .HasColumnName("author_name");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.bookId).HasName("PRIMARY");

            entity.ToTable("books");

            entity.HasIndex(e => new { e.authorId, e.categoryId }, "author_id");

            entity.HasIndex(e => e.categoryId, "category_id");

            entity.Property(e => e.bookId)
                .HasColumnType("int(11)")
                .HasColumnName("book_id");
            entity.Property(e => e.authorId)
                .HasColumnType("int(11)")
                .HasColumnName("author_id");
            entity.Property(e => e.categoryId)
                .HasColumnType("int(11)")
                .HasColumnName("category_id");
            entity.Property(e => e.publishDate)
                .HasColumnType("date")
                .HasColumnName("publish_date");
            entity.Property(e => e.title)
                .HasMaxLength(200)
                .HasColumnName("title");

            entity.HasOne(d => d.author).WithMany(p => p.books)
                .HasForeignKey(d => d.authorId)
                .HasConstraintName("books_ibfk_1");

            entity.HasOne(d => d.category).WithMany(p => p.books)
                .HasForeignKey(d => d.categoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("books_ibfk_2");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.categoryId).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.Property(e => e.categoryId)
                .HasColumnType("int(11)")
                .HasColumnName("category_id");
            entity.Property(e => e.categoryName)
                .HasMaxLength(100)
                .HasColumnName("category_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
