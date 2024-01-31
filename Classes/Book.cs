using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Classes
{
    internal class Book
    {
        public ulong ISBSN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int NumberOfCopies { get; set; }
        public int BarrowedCopies { get; set; }

    }
}
