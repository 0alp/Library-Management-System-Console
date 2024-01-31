using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Classes
{
    internal class BorrowingTransaction
    {
        public int OrderID { get; set; }
        public ulong ISBN { get; set; }
        public string BookName { get; set; }
        public string NameOfBorrower { get; set; }
        public string LoanDate { get; set; }
        public string DueDate { get; set; }

        public string Status { get; set; }  
    }
}
