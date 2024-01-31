using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp.Classes
{
    
    internal class Library
    {
        private JsonHandler<Book> bookJsonHandler;
        private JsonHandler<BorrowingTransaction> borrowingTransactionJsonHandler;
        public Library()
        {
            bookJsonHandler = new JsonHandler<Book>("books.json");
            borrowingTransactionJsonHandler = new JsonHandler<BorrowingTransaction>("orders.json");
        }

        public List<Book> DeserializedBooks { get { return bookJsonHandler.Data; } }

        public void AddBook(Book book)
        {
            bookJsonHandler.AddItem(book);
        }

        public void DisplayBooks()
        {
            bookJsonHandler.ReadJsonFile();

            var table = new ConsoleTable("ISBSN", "Title", "Author", "NumberOfCoppies", "barrowedCoppies");

            if (bookJsonHandler.Data == null)
                return;

            foreach (var item in bookJsonHandler.Data)
            {
                if (item is Book book)
                    table.AddRow(book.ISBSN, book.Title, book.Author, book.NumberOfCopies, book.BarrowedCopies);
            }

            table.Write();
            Console.WriteLine();
        }
        public void DisplayBooks(List<Book> book)
        {
            bookJsonHandler.ReadJsonFile();

            var table = new ConsoleTable("ISBSN", "Title", "Author", "NumberOfCoppies", "barrowedCoppies");

            if (bookJsonHandler.Data == null)
                return;

            foreach (var item in book)
            {
                   table.AddRow(item.ISBSN, item.Title, item.Author, item.NumberOfCopies, item.BarrowedCopies);
            }

            table.Write();
            Console.WriteLine();
        }

        public void ReceiveBook(ulong ISBN, int orderID)
        {
            bookJsonHandler.ReadJsonFile();
            Book borrowedBook = bookJsonHandler.Data.Find(x => x.ISBSN == ISBN);
            BorrowingTransaction b= borrowingTransactionJsonHandler.Data.Find(x => x.OrderID == orderID);
            b.Status = "Received";
            borrowingTransactionJsonHandler.SaveChanges();
            borrowedBook.NumberOfCopies ++;
            borrowedBook.BarrowedCopies --;
            bookJsonHandler.SaveChanges();
        }

        public void LendBook(ulong ISBN, int numberOfBooks, BorrowingTransaction borrowingTransaction)
        {
            bookJsonHandler.ReadJsonFile();
            Book givenBook = bookJsonHandler.Data.Find(x => x.ISBSN == ISBN);
            borrowingTransactionJsonHandler.AddItem(borrowingTransaction);
            if (givenBook.NumberOfCopies < numberOfBooks)
            {
                Console.WriteLine("Error: You cannot lend more books than the number of copies!\n\n");
                return;
            }

            givenBook.NumberOfCopies -= numberOfBooks;
            givenBook.BarrowedCopies += numberOfBooks;
            bookJsonHandler.SaveChanges();
        }

        public List<Book>? FindBooksByAuthor(string Author)
        {
            bookJsonHandler.ReadJsonFile();
            List<Book> books = bookJsonHandler.Data?.Where(x => x.Author == Author).ToList();
            return books;
        }

        public List<Book> FindBooksByTitle(string Title)
        {
            bookJsonHandler.ReadJsonFile();
            List<Book> books = bookJsonHandler.Data?.Where(x => x.Title == Title).ToList();
            return books;
        }

        public List<BorrowingTransaction> FindOverdueBooks()
        {
            borrowingTransactionJsonHandler.ReadJsonFile();

            if (borrowingTransactionJsonHandler.Data == null)
                return null;

            foreach (var item in borrowingTransactionJsonHandler.Data)
            {

               
                if (Convert.ToDateTime(item.DueDate) <= DateTime.Now)
                {
                    Console.WriteLine($"\nORDER ID :{item.OrderID} \t BOOK NAME : {item.BookName} \t NAME OF BORROWER : {item.NameOfBorrower} .WARNING : DELAYED TIME !\n\a");

                }
                else
                {

                    Console.WriteLine("There are no books past due date");
                }
            }

            return null;
        }
    }
}
