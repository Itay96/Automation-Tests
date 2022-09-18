using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libary
{
   public class Book
    {

        #region Enum
        // A book can be defined as a children book or an adult book
        public enum BookType
        {
            ChildrenBook,
            AdultBook
        }

        //The book can be in diffrent statuses:
        public enum BookStatus
        {
            InTheLibary,//על המדף
            Reserved,//מוזמן
            OutOfTheLibary,//מחוץ לספרייה- מישהו השאיל
            OutOfTheLibaryAndReserved,//מחוץ לספרייה כי מישהו השאיל ובינתיים מישהו אחר הזמין את הספר


        }
        #endregion


        #region PrivateVariables
        private string id;
        private BookType type;
        private BookStatus status;
        #endregion

        #region Properties
        public string Id
        {
            get => id;
        }

         public BookType Type
         {
            get => type;
         }

        public BookStatus Status
        {
            get => status;
            set { status = value; }
        }

            #endregion

            #region Consructors

        public Book()
        {
            //Genrate a random string that will as id
            Guid guid = Guid.NewGuid();
            id = guid.ToString();

            type = BookType.AdultBook;
            status = BookStatus.InTheLibary;

        }

        public Book(BookType type,BookStatus status)
        {
            Guid guid = Guid.NewGuid();
            id = guid.ToString();

            this.type = type;
            this.status = status;
        }

            #endregion


        }
}
