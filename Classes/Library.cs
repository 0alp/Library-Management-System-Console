using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibraryApp.Classes
{
    internal class Library
    {
        public Library() 
        {
            
            if (System.IO.File.Exists("books.json") == false)
                System.IO.File.Create("books.json").Close();
            deserialized = new();
            ReadJsonFile();

        }
        List<Book> deserialized;
        public List<Book> DeserializedBooks { get { return deserialized; } }
        public void AddBook(Book book)
        {
            AddToJsonFile(book);
           
        }
        
        public void DisplayBooks(List<Book> bookList)
        {
            
            var table = new ConsoleTable("ISBSN", "Title", "Author", "NumberOfCoppies",
                "barrowedCoppies");
            if (bookList == null)
                return;
            foreach (var item in bookList)
            {


                table.AddRow(item.ISBSN, item.Title, item.Author, item.NumberOfCopies,
                   item.BarrowedCopies);
                

            }
            table.Write();
            Console.WriteLine();

        }

        public void ReceiveBook(int ISBN, int numberOfBooks)
        {
            
            ReadJsonFile();
            Book barrowedBook = deserialized.Find(x => x.ISBSN == ISBN);
            if (barrowedBook.BarrowedCopies < numberOfBooks)
            {
                Console.WriteLine("Error : The number of books returned cannot be more than the number of books lent !\n\n");
                return;
            }
                
            barrowedBook.NumberOfCopies += numberOfBooks;
            barrowedBook.BarrowedCopies -= numberOfBooks;
            AddToJsonFile();
            
        }



        public void LendBook(int ISBN, int numberOfBooks)
        {
            ReadJsonFile();
            Book givenBook = deserialized.Find(x => x.ISBSN == ISBN);
            if (givenBook.NumberOfCopies < numberOfBooks)
            {
                Console.WriteLine("Error : You do not lend more books than the number of copies ! \n\n");
                return;
            }

            givenBook.NumberOfCopies -= numberOfBooks;
            givenBook.BarrowedCopies += numberOfBooks;
            AddToJsonFile();

        }
        public List<Book>? FindBooksByAuthor(string Author)
        {
            var table = new ConsoleTable("ISBSN", "Title", "Author", "NumberOfCoppies",
                "barrowedCoppies");
            if (deserialized == null)
                return null;
            ReadJsonFile();
            List<Book> books = deserialized.Where(x => x.Author == Author).ToList();
            return books;
        }

        public List<Book> FindBooksByTitle(string Title)
        {
            var table = new ConsoleTable("ISBSN", "Title", "Author", "NumberOfCoppies",
                "barrowedCoppies");
            if (deserialized == null)
                return null;
            ReadJsonFile();
            List<Book> books = deserialized.Where(x => x.Title == Title).ToList();
            return books;
        }

        private void ReadJsonFile()
        {
            try
            {
                if (deserialized == null)
                    return;
                string json = System.IO.File.ReadAllText("books.json");
                deserialized = JsonSerializer.Deserialize<List<Book>>(json);
                

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void AddToJsonFile(Book book)
        {
            ReadJsonFile();
            deserialized.Add(book); 
            string updatedJson = JsonSerializer.Serialize<List<Book>>(deserialized);
            System.IO.File.WriteAllText("books.json", updatedJson);
            


        }
        private void AddToJsonFile()
        {

            string updatedJson = JsonSerializer.Serialize<List<Book>>(deserialized);
            System.IO.File.WriteAllText("books.json", updatedJson);


        }

      

    }



    }

