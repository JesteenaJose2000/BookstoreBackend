﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    internal interface IBookRepository
    {
        List<Book> GetBestBooks();
        List<Book> GetAllBooks();
        List<Book> GetBookByCat(int CatId);
        List<Book> GetBookByName(string Title);
        List<Book> GetBookByAuthor(string Author);
        Book GetBookByISBN(string ISBN);
        Book GetBookById(int BookId);
        Book AddBook(Book book);
        void UpdateBook(int BookId,Book book);
        void DeleteBook(int BookId);
    }
}
