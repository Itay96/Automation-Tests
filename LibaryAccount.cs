using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary
{
   public class LibaryAccount
    {
        #region Private Variabels
        private const int MAX_NUM_OF_LOAN_BOOKS = 3;
        private const int MAX_NUM_OF_RESERVED_BOOKS = 3;
        private Reader owner;
        private List<Book> loanedBooks;
        private List<Book> reservedBooks;
        private double ownerDebt;
        #endregion


        #region Properties
        public int MaxNumOfLoanBooks
        {
            get { return MAX_NUM_OF_LOAN_BOOKS; }
        }
        public int MaxNumOfReservedBooks
        {
            get { return MAX_NUM_OF_RESERVED_BOOKS; }
        }

        public Reader Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public List<Book> LoanBooks
        {
            get { return loanedBooks; }
            set { loanedBooks = value; }
        }

        public List<Book> ReservedBooks
        {
            get { return reservedBooks; }
            set { reservedBooks = value; }
        }

        public double Debt
        {
            get { return ownerDebt; }
            set { ownerDebt = value; }
        }

        #endregion


        #region Constructors

        public LibaryAccount(Reader reader)
        {
            this.Owner = reader;
            this.LoanBooks = new List<Book>();
            this.ReservedBooks = new List<Book>();
            this.Debt = 0;//אין חוב ללקוח חדש
        }
        #endregion


        #region Methods

        public bool LoanBook(Book bookToLoan)
        {
            #region Validations according to task
            //לממש לבד:
            //ensure that the book in the library
            //or
            // ensure that the book is reserved to me (according to the content of my reserved books)

            if (bookToLoan.Status != Book.BookStatus.InTheLibary && bookToLoan.Status != Book.BookStatus.Reserved)
                throw new InvalidOperationException("The Book Is Not In The Libary");
            
            if (bookToLoan.Status == Book.BookStatus.Reserved && !reservedBooks.Contains(bookToLoan))
                throw new InvalidOperationException("The Book reserved but NOT in reserved list");


            // לממש לבד:
            //ensure that this book is NOT in my loan Books list
            if (loanedBooks.Contains(bookToLoan))
                throw new InvalidOperationException("The Book in in my loan books list");

            //validation - Check that the reader does dosent have more then the max number of loan books
            if (loanedBooks.Count == MaxNumOfLoanBooks)
                throw new InvalidOperationException("Reader already has " + MaxNumOfLoanBooks + "max (max number)");

            //validation - Check that the cuurent libaryAccount does dosent have any debt
            if (Debt > 0)
                throw new InvalidOperationException("Reader alreay has " + Debt + "NIS,  so he cannot loan books");
            #endregion

            //לממש לבד:
            //ensure that the reader type match the book type (adult reader can only read adult book)
            if (Owner.Type == Reader.ReaderType.Adult)
            {
                if (bookToLoan.Type != Book.BookType.AdultBook)
                    throw new InvalidOperationException("The Book Type Is Not Equals To Adult Book");
            }

            else 
            {
                if (bookToLoan.Type != Book.BookType.ChildrenBook)
                    throw new InvalidOperationException("The Book Type Is Not Equals To Children Book");
            }
    

            //if the code came to this place - then we can procced with book loan activity

            //לממש לבד עדכון סטאטוס של הספר כדי לסמן שהוא כרגע נלקח
            bookToLoan.Status = Book.BookStatus.OutOfTheLibary;
            loanedBooks.Add(bookToLoan);

            //לממש לבד- אם הספר נמצא ברשימת הספרים המוזמנים שלי,יש להסיר אותו משם
            if(reservedBooks.Contains(bookToLoan))
                loanedBooks.Remove(bookToLoan);
            
          


            return true;
        }

        public bool ReturnBook(Book bookToReturn)
        {

            #region Validtions ensure if the return activity can be proceeded

            //Return book proccess should be allowd only if the very same book is in the loan list of the reader

            if (!loanedBooks.Contains(bookToReturn))
                throw new InvalidOperationException("we cannot return a book that is Not in our loan list books");
                #endregion
            if (bookToReturn.Status != Book.BookStatus.OutOfTheLibary &&
                bookToReturn.Status != Book.BookStatus.OutOfTheLibaryAndReserved)
            {
                throw new InvalidOperationException("");
            }

            //if the code came to this place - then we can proceed with the return book activity
            //book status updeted:
            if (bookToReturn.Status == Book.BookStatus.OutOfTheLibary)
                bookToReturn.Status = Book.BookStatus.InTheLibary;//היה מחוץ לספרייה ,כעת זמין
            else if (bookToReturn.Status == Book.BookStatus.OutOfTheLibaryAndReserved)
                bookToReturn.Status = Book.BookStatus.Reserved;//היה בחוץ, ותוך כדי גם הוזמן ולכן כשהוחזר הסטטאטוס שלו השתנה למוזמן

            //לממש לבד:
            //להסיר את הספר מהרשימה של הקורא
            if(loanedBooks.Contains(bookToReturn))
                  loanedBooks.Remove(bookToReturn);

            return true;
        }

        public bool OrderBook(Book bookToOrder)
        {
            Console.WriteLine("---------------------",bookToOrder.Status);
            if (bookToOrder.Status != Book.BookStatus.OutOfTheLibary && bookToOrder.Status != Book.BookStatus.InTheLibary)
                throw new InvalidOperationException("The Book Is Not In The Right Status");


            if (Owner.Type == Reader.ReaderType.Adult)
            {
                if (bookToOrder.Type != Book.BookType.AdultBook)
                    throw new InvalidOperationException("The Book Type Is Not Equals To Adult Book");
            }

            else
            {
                if (bookToOrder.Type != Book.BookType.ChildrenBook)
                    throw new InvalidOperationException("The Book Type Is Not Equals To Children Book");
            }


            if (reservedBooks.Count == MaxNumOfReservedBooks)
                throw new InvalidOperationException("Reader already has " + MaxNumOfLoanBooks + "max (max number)");

            //validation - Check that the cuurent libaryAccount does dosent have any debt
            if (Debt > 0)
                throw new InvalidOperationException("Reader alreay has " + Debt + "NIS,  so he cannot loan books");

            if (bookToOrder.Status == Book.BookStatus.OutOfTheLibary)
                bookToOrder.Status = Book.BookStatus.OutOfTheLibaryAndReserved;//היה מחוץ לספרייה ,כעת זמין
            else if (bookToOrder.Status == Book.BookStatus.InTheLibary)
                bookToOrder.Status = Book.BookStatus.Reserved;//היה בחוץ, ותוך כדי גם הוזמן ולכן כשהוחזר הסטטאטוס שלו השתנה למוזמן

            reservedBooks.Add(bookToOrder);

            return true;
        }



        public bool CancelBook(Book bookToCancel)
        {

            if (!reservedBooks.Contains(bookToCancel))
                throw new InvalidOperationException("we cannot return a book that is Not in our loan list books");

            if (bookToCancel.Status == Book.BookStatus.OutOfTheLibaryAndReserved)
                bookToCancel.Status = Book.BookStatus.OutOfTheLibary;//היה מחוץ לספרייה ,כעת זמין
            else if (bookToCancel.Status == Book.BookStatus.Reserved)
                bookToCancel.Status = Book.BookStatus.InTheLibary;//היה בחוץ, ותוך כדי גם הוזמן ולכן כשהוחזר הסטטאטוס שלו השתנה למוזמן

            reservedBooks.Remove(bookToCancel);


            return true;
        }


        #endregion
    }
}
