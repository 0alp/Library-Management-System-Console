using System;
using System.Collections.Generic;
using LibraryApp.Classes;
using ConsoleTables;

class Program
{
    static void Main()
    {
        Book book = new Book();
        Library library = new Library();
        int selectedAction = 0;
        ulong ISBN = 0;
        BorrowingTransaction borrowingTransaction = new BorrowingTransaction(); 
        Console.WriteLine(" _ _ _                                                           \r\n| (_) |__  _ __ __ _ _ __ _   _                                  \r\n| | | '_ \\| '__/ _` | '__| | | |                                 \r\n| | | |_) | | | (_| | |  | |_| |                                 \r\n|_|_|_.__/|_|  \\__,_|_|   \\__, |                                 \r\n                          |___/                                  \r\n                                                             _   \r\n _ __ ___   __ _ _ __   __ _  __ _  ___ _ __ ___   ___ _ __ | |_ \r\n| '_ ` _ \\ / _` | '_ \\ / _` |/ _` |/ _ \\ '_ ` _ \\ / _ \\ '_ \\| __|\r\n| | | | | | (_| | | | | (_| | (_| |  __/ | | | | |  __/ | | | |_ \r\n|_| |_| |_|\\__,_|_| |_|\\__,_|\\__, |\\___|_| |_| |_|\\___|_| |_|\\__|\r\n                             |___/                               \r\n               _                                                 \r\n ___ _   _ ___| |_ ___ _ __ ___                                  \r\n/ __| | | / __| __/ _ \\ '_ ` _ \\                                 \r\n\\__ \\ |_| \\__ \\ ||  __/ | | | | |                                \r\n|___/\\__, |___/\\__\\___|_| |_| |_|                                \r\n     |___/                              ");

        Console.WriteLine("\n\n Please press any key to start");
        Console.ReadKey();
        Console.Clear();
        string c = "Please select the action you want to take:\n" +
        "1-) Add a book.\n" +
        "2-) Receive the book.\n" +
        "3-) Lend a book.\n" +
        "4-) Display all books.\n" +
        "5-) Display books whose due dates have passed.\n" +
        "6-) Find a book.\n";

        while (true)
        {
            Console.WriteLine(c);
            try
            {
                selectedAction = Convert.ToInt32(Console.ReadLine());


                switch (selectedAction)
                {
                    case 1:
                        Console.WriteLine("Please enter the ISBN of the book to add :\t\t");
                        book.ISBSN = ulong.Parse(Console.ReadLine());

                        Console.WriteLine("Please enter the Title of the book to add :\t\t");
                        book.Title = Console.ReadLine();

                        Console.WriteLine("Please enter the Author of the book to add :\t\t");
                        book.Author = Console.ReadLine();

                        Console.WriteLine("Please enter the NumberOfCoppies of the book to add :\t\t");
                        book.NumberOfCopies = int.Parse(Console.ReadLine());

                        Console.WriteLine("Please enter the BarrowedCoppies of the book to add :\t\t");
                        book.BarrowedCopies = int.Parse(Console.ReadLine());

                        library.AddBook(book);
                        break;

                    case 2:
                        Console.WriteLine("Please enter the ISBN of the returned book");
                        ISBN = ulong.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter the ORDER ID");
                        int orderID = int.Parse(Console.ReadLine());
                        library.ReceiveBook(ISBN, orderID);
                        Console.WriteLine("\n The book was successfully retrieved\n");
                        break;

                    case 3:
                        Random random = new Random();
                        Console.WriteLine("Please enter the ISBN of the book to be given\t\t");
                        ISBN = ulong.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter the number of books given");
                        int numberOfBooksGiven = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter the name of the book to be given\t\t");
                        borrowingTransaction.BookName = Console.ReadLine();
                        borrowingTransaction.LoanDate = DateTime.Now.ToShortDateString();
                        borrowingTransaction.OrderID = random.Next(10000, 100000);
                        borrowingTransaction.Status = "not received";
                        borrowingTransaction.ISBN = ISBN;
                        Console.WriteLine("Please enter the name of borrower");
                        borrowingTransaction.NameOfBorrower = Console.ReadLine();
                        Console.WriteLine("Please enter the due date (DAY-MONTH-YEAR)");
                        borrowingTransaction.DueDate = Console.ReadLine();
                        library.LendBook(ISBN, numberOfBooksGiven, borrowingTransaction);
                        Console.WriteLine("\n The book has been lent successfully\n");
                        break;

                    case 4:
                        library.DisplayBooks();
                        break;

                    case 5:
                        library.FindOverdueBooks();

                        break;

                    case 6:
                        List<Book> books;
                        Console.WriteLine("1-)Find the books by author\n" +
                            "2-)Find the book by Title");
                        int findOption = Convert.ToInt32(Console.ReadLine());
                        if (findOption == 1)
                        {
                            Console.WriteLine("Please enter the author's name");
                            books = library.FindBooksByAuthor(Console.ReadLine());
                            library.DisplayBooks(books);
                        }
                        else if (findOption == 2)
                        {
                            Console.WriteLine("Please enter the title of the book");
                            books = library.FindBooksByTitle(Console.ReadLine());
                            library.DisplayBooks(books);
                        }
                        else
                        {
                            Console.WriteLine("Please make a valid choice. (1 or 2)");
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Please enter data in the appropriate format. The error you get: {e.Message} ");
            }


        }
    }
}
