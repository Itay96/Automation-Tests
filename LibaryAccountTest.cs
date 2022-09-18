using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace Libary.UnitTest
{
    [TestClass]
    public class LibararyAccountTest
    {
        #region DataRow Test

        [DataRow (Reader.ReaderType.Child,Book.BookType.ChildrenBook,Book.BookStatus.InTheLibary,"itay","M",
            DisplayName = "ChildTriesToGetAvailableChildrenBook_ReturnTrueAndUpdateList")]
        [DataRow(Reader.ReaderType.Adult, Book.BookType.AdultBook, Book.BookStatus.InTheLibary, "itay", "R",
            DisplayName = "AdultTriesToGetAvailableAdultBook_ReturnTrueAndUpdateList")]
        [TestMethod]
        public void LoanBook(Reader.ReaderType readerType, Book.BookType bookType , Book.BookStatus bookStatus,String firstName, String lastName)
        {

            //Arrange
            Reader childReader = new Reader(firstName,lastName, readerType); //Initialize a child reader
            LibaryAccount childLibaryAccount = new LibaryAccount(childReader); //Create a libary account for a child reader
            Book childrenBook = new Book(bookType, bookStatus);

            bool actual;

            //Act
            actual = childLibaryAccount.LoanBook(childrenBook);


            //Assert
            Assert.IsTrue(actual, "BUG: a child should be able loan a children book");
            Assert.IsTrue(childLibaryAccount.LoanBooks.Contains(childrenBook), "BUG: The loan proccess is a success however list is not updated");
            Assert.AreEqual(childrenBook.Status, Book.BookStatus.OutOfTheLibary, "BUG: the book wat loan howeven its status was not updated");
            Assert.IsFalse(childLibaryAccount.ReservedBooks.Contains(childrenBook), "BUG: the child took the book, however it appears in his reservation list");
        }



        #endregion


        #region LoanBooks
        [TestMethod]
        public void loanbook_statusInTheLibary_ReturnTrue()
        {

            //Arrange
            Reader adultReader = new Reader("itay", "Monis", Reader.ReaderType.Adult); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(adultReader); //Create a libary account for a child reader
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.InTheLibary);
            Book bookToAdd = new Book();
            adultLibaryAccount.ReservedBooks.Add(bookToAdd);
            bool actual;

            //Act
            actual = adultLibaryAccount.LoanBook(adultBook);


            //Assert
            Assert.IsTrue(actual, "BUG: you can lowan the book");
            Assert.AreEqual(adultBook.Status, Book.BookStatus.OutOfTheLibary, "BUG: the book was loan howeven its status was not updated");
            Assert.IsFalse(adultLibaryAccount.ReservedBooks.Contains(adultBook), "BUG: the adult took the book, however it appears in his reservation list");
            Assert.IsTrue(adultLibaryAccount.LoanBooks.Contains(adultBook));
        }

        [TestMethod]
        public void loanbook_statusIsReservedButNotInReservedList_ThrowsInvalidOperationException()
        {

            //Arrange
            Reader adultReader = new Reader("itay", "Monis", Reader.ReaderType.Adult); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(adultReader); //Create a libary account for a child reader
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.Reserved);
            bool actual;


            Assert.ThrowsException<InvalidOperationException>(() => adultLibaryAccount.LoanBook(adultBook));
            Assert.IsFalse(adultLibaryAccount.LoanBooks.Contains(adultBook));
            Assert.IsTrue(adultBook.Status == Book.BookStatus.Reserved);

        }



        [TestMethod]
        public void LoanBook_ChildTriesToGetAvailableChildrenBook_ReturnTrueAndUpdateList()
        {

            //Arrange
            Reader childReader = new Reader("itay", "M", Reader.ReaderType.Child); //Initialize a child reader
            LibaryAccount childLibaryAccount = new LibaryAccount(childReader); //Create a libary account for a child reader
            Book childrenBook = new Book(Book.BookType.ChildrenBook, Book.BookStatus.InTheLibary);

            bool actual;

            //Act
            actual = childLibaryAccount.LoanBook(childrenBook);


            //Assert
            Assert.IsTrue(actual,"BUG: a child should be able loan a children book");
            Assert.IsTrue(childLibaryAccount.LoanBooks.Contains(childrenBook), "BUG: The loan proccess is a success however list is not updated");
            Assert.AreEqual(childrenBook.Status, Book.BookStatus.OutOfTheLibary, "BUG: the book wat loan howeven its status was not updated");
            Assert.IsFalse(childLibaryAccount.ReservedBooks.Contains(childrenBook), "BUG: the child took the book, however it appears in his reservation list");
        }


        [TestMethod]
        public void LoanBook_AdultdTriesToGetAvailableAdultBook_ReturnTrueAndUpdateList()
        {

            //Arrange
            Reader adultReader = new Reader("itay", "Monis", Reader.ReaderType.Adult); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(adultReader); //Create a libary account for a child reader
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.InTheLibary);

            bool actual;

            //Act
            actual = adultLibaryAccount.LoanBook(adultBook);


            //Assert
            Assert.IsTrue(actual, "BUG: a adult should be able loan a Adult book");
            Assert.IsTrue(adultLibaryAccount.LoanBooks.Contains(adultBook), "BUG: The loan proccess is a success however list is not updated");
            Assert.AreEqual(adultBook.Status, Book.BookStatus.OutOfTheLibary, "BUG: the book wat loan howeven its status was not updated");
            Assert.IsFalse(adultLibaryAccount.ReservedBooks.Contains(adultBook), "BUG: the adult took the book, however it appears in his reservation list");
        }


        [TestMethod]
        public void LoanBook_AdultdTriesToGetAvailableChildBook_ReturnTrueAndUpdateList()
        {

            //Arrange
            Reader adultReader = new Reader("itay", "Monis", Reader.ReaderType.Adult); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(adultReader); //Create a libary account for a child reader
            Book childbook = new Book(Book.BookType.ChildrenBook, Book.BookStatus.Reserved);
            Book bookToAdd = new Book();
            adultLibaryAccount.LoanBooks.Add(bookToAdd);

            //Assert&&Act

            Assert.ThrowsException<InvalidOperationException>(() => adultLibaryAccount.LoanBook(childbook));
            Assert.IsFalse(adultLibaryAccount.LoanBooks.Contains(childbook));
            Assert.IsTrue(childbook.Status == Book.BookStatus.Reserved);
        }


        [TestMethod]
        public void LoanBook_TheReaderHasAbove3Book_returnTrue()
        {

            //Arrange
            Reader adultReader = new Reader("itay", "Monis", Reader.ReaderType.Adult); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(adultReader); //Create a libary account for a child reader
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.InTheLibary);
            Book bookToAdd = new Book();

            adultLibaryAccount.LoanBooks.Add(bookToAdd);
            adultLibaryAccount.LoanBooks.Add(bookToAdd);
            adultLibaryAccount.LoanBooks.Add(bookToAdd);


            //Assert&&Act
            Assert.ThrowsException< InvalidOperationException > (() => adultLibaryAccount.LoanBook(adultBook));
            Assert.IsFalse(adultLibaryAccount.LoanBooks.Contains(adultBook));
            Assert.IsTrue(adultBook.Status == Book.BookStatus.InTheLibary);

        }



        [TestMethod]
        public void LoanBook_AndTheReaderHasADebt_ThrowInvalidOperationException()
        {

            //Arrange
            Reader adultReader = new Reader("itay", "Monis", Reader.ReaderType.Adult); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(adultReader); //Create a libary account for a child reader
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.InTheLibary);

            adultLibaryAccount.Debt = 1;
            


            //Assert&&Act
            Assert.ThrowsException<InvalidOperationException>(() => adultLibaryAccount.LoanBook(adultBook));
            Assert.IsFalse(adultLibaryAccount.LoanBooks.Contains(adultBook));
            Assert.IsTrue(adultBook.Status == Book.BookStatus.InTheLibary);

        }
        #endregion

        #region ReturnBooks
        [TestMethod]
        public void ReturnBook_TheReaderReturnTheBookSuccessfully_ReturnTrue()
        {

            //Arrange
            Reader adultReader = new Reader("itay", "Monis", Reader.ReaderType.Adult); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(adultReader); //Create a libary account for a child reader
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.OutOfTheLibary);

            adultLibaryAccount.LoanBooks.Add(adultBook);

            bool actual,
                expected=true;
           

            //Act
            actual = adultLibaryAccount.ReturnBook(adultBook);


            //Assert

            Assert.IsFalse(adultLibaryAccount.LoanBooks.Contains(adultBook));
            Assert.AreEqual(expected,actual);
            if (adultBook.Status == Book.BookStatus.OutOfTheLibary)
                Assert.IsTrue(adultBook.Status == Book.BookStatus.InTheLibary);
            else
                if(adultBook.Status == Book.BookStatus.OutOfTheLibaryAndReserved)
                Assert.IsTrue(adultBook.Status == Book.BookStatus.Reserved);
                

        }


        [TestMethod]
        public void ReturnBook_TheReaderCanNotReturnsTheBook_ThrowInvalidOperationException()
        {

            //Arrange
            Reader childReader = new Reader("itay", "Monis", Reader.ReaderType.Child); //Initialize a child reader
            LibaryAccount childLibraryAccount = new LibaryAccount(childReader); //Create a libary account for a child reader
            Book childBook = new Book(Book.BookType.ChildrenBook, Book.BookStatus.InTheLibary);


            //Act&&Assert

            Assert.ThrowsException<InvalidOperationException>(() => childLibraryAccount.ReturnBook(childBook));
            Assert.IsFalse(childLibraryAccount.LoanBooks.Contains(childBook));
            Assert.IsTrue(childBook.Status == Book.BookStatus.InTheLibary);

        }


        [TestMethod]
        public void ReturnBook_BookIsOutOfTheLibararyButNotInTheLoanList_ThrowInvalidOperationException()
        {

            //Arrange
            Reader adultReader = new Reader("itay", "Monis", Reader.ReaderType.Adult); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(adultReader); //Create a libary account for a child reader
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.OutOfTheLibary);



            //Assert
            Assert.ThrowsException<InvalidOperationException>(() => adultLibaryAccount.ReturnBook(adultBook));
            Assert.IsFalse(adultLibaryAccount.LoanBooks.Contains(adultBook));
            Assert.IsTrue(adultBook.Status == Book.BookStatus.OutOfTheLibary);

        }
        [TestMethod]
        public void OrderBook_TheReaderCanOrderTheBookSuccessfully_ReturnTrue()
        {

            //Arrange
            Reader adultReader = new Reader("itay", "Monis", Reader.ReaderType.Adult); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(adultReader); //Create a libary account for a child reader
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.OutOfTheLibary);

            //Act
            bool actual,
                expected = true;

            actual = adultLibaryAccount.OrderBook(adultBook);


            //Assert

            Assert.IsTrue(actual == expected);
            Assert.IsTrue(adultLibaryAccount.ReservedBooks.Contains(adultBook));

            if (adultBook.Status == Book.BookStatus.OutOfTheLibary)
                Assert.IsTrue(adultBook.Status == Book.BookStatus.OutOfTheLibaryAndReserved);
            else if (adultBook.Status == Book.BookStatus.InTheLibary)
                Assert.IsTrue(adultBook.Status == Book.BookStatus.Reserved);

        }

        #endregion

        #region OrderBooks
        [TestMethod]
        public void OrderBook_TheRedaerCanBeOrederTheBook_returnTrue()
        {

            //Arrange
            Reader adultReader = new Reader("itay", "Monis", Reader.ReaderType.Adult); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(adultReader); //Create a libary account for a child reader
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.InTheLibary);

            //Act
            bool actual,
                expected = true;

            actual = adultLibaryAccount.OrderBook(adultBook);


            //Assert

            Assert.IsTrue(actual == expected);
            Assert.IsTrue(adultLibaryAccount.ReservedBooks.Contains(adultBook));

            if (adultBook.Status == Book.BookStatus.OutOfTheLibary)
                Assert.IsTrue(adultBook.Status == Book.BookStatus.OutOfTheLibaryAndReserved);
            else if (adultBook.Status == Book.BookStatus.InTheLibary)
                Assert.IsTrue(adultBook.Status == Book.BookStatus.Reserved);

        }

        //Test With TimeOut Attribute
        [TestMethod]
        [Timeout(1000)]
        public void OrderBook_childTryToOrderAdultBook_ThrowInvalidOperationException()
        {

            //Arrange
            Reader childReader = new Reader("itay", "Monis", Reader.ReaderType.Child); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(childReader); //Create a libary account for a child reader
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.InTheLibary);


            //Assert&&Act

            Assert.ThrowsException<InvalidOperationException>(() => adultLibaryAccount.OrderBook(adultBook));
            Assert.IsFalse(adultLibaryAccount.ReservedBooks.Contains(adultBook));
            Assert.IsTrue(adultBook.Status == Book.BookStatus.InTheLibary);
            
        }


        [TestMethod]
        public void OrderBook_adultTryToOrderChildBook_ThrowInvalidOperationException()
        {

            //Arrange
            Reader adultReader = new Reader("itay", "Monis", Reader.ReaderType.Adult); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(adultReader); //Create a libary account for a child reader
            Book childBook = new Book(Book.BookType.ChildrenBook, Book.BookStatus.InTheLibary);


            //Assert&&Act

            Assert.ThrowsException<InvalidOperationException>(() => adultLibaryAccount.OrderBook(childBook));
            Assert.IsFalse(adultLibaryAccount.ReservedBooks.Contains(childBook));
            Assert.IsTrue(childBook.Status == Book.BookStatus.InTheLibary);


        }

        [TestMethod]
        public void OrderBook_TheReaderHasAlready3BooksInHisList_ThrowInvalidOperationException()
        {

            //Arrange
            Reader adultReader = new Reader("itay", "Monis", Reader.ReaderType.Adult); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(adultReader); //Create a libary account for a child reader
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.OutOfTheLibary);

            Book bookToAdd = new Book();

            adultLibaryAccount.ReservedBooks.Add(bookToAdd);
            adultLibaryAccount.ReservedBooks.Add(bookToAdd);
            adultLibaryAccount.ReservedBooks.Add(bookToAdd);
            

            //Assert&&Act

            Assert.ThrowsException<InvalidOperationException>(() => adultLibaryAccount.OrderBook(adultBook));
            Assert.IsFalse(adultLibaryAccount.ReservedBooks.Contains(adultBook));
            Assert.IsTrue(adultBook.Status == Book.BookStatus.OutOfTheLibary);

        }

        [TestMethod]
        public void OrderBook_TheReaderHasADebt_ThrowInvalidOperationException()
        {

            //Arrange
            Reader adultReader = new Reader("itay", "Monis", Reader.ReaderType.Adult); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(adultReader); //Create a libary account for a child reader
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.OutOfTheLibary);

            adultLibaryAccount.Debt = 20;
           

            //Assert&&Act

            Assert.ThrowsException<InvalidOperationException>(() => adultLibaryAccount.OrderBook(adultBook));
            Assert.IsFalse(adultLibaryAccount.ReservedBooks.Contains(adultBook));
            Assert.IsTrue(adultBook.Status == Book.BookStatus.OutOfTheLibary);


        }

        #endregion

        #region CancelBooks
        [TestMethod]
        public void CancelBook_TheReaderCanCancelABook_ReturnTrue()
        {

            //Arrange
            Reader adultReader = new Reader("itay", "Monis", Reader.ReaderType.Adult); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(adultReader); //Create a libary account for a child reader
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.Reserved);
            bool actual;


            adultLibaryAccount.ReservedBooks.Add(adultBook);
            actual = adultLibaryAccount.CancelBook(adultBook);

            //Assert&&Act

            Assert.IsTrue(actual);
            Assert.IsFalse(adultLibaryAccount.ReservedBooks.Contains(adultBook));
            Assert.IsTrue(adultBook.Status == Book.BookStatus.InTheLibary);




        }


        [TestMethod]
        public void CancelBook_TheReaderCanCancelABookReserved_ReturnTrue()
        {

            //Arrange
            Reader adultReader = new Reader("itay", "Monis", Reader.ReaderType.Adult); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(adultReader); //Create a libary account for a child reader
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.OutOfTheLibaryAndReserved);
            bool actual;

            adultLibaryAccount.ReservedBooks.Add(adultBook);
            actual = adultLibaryAccount.CancelBook(adultBook);

            //Assert&&Act

            Assert.IsTrue(actual);
            Assert.IsFalse(adultLibaryAccount.ReservedBooks.Contains(adultBook));
            Assert.IsTrue(adultBook.Status == Book.BookStatus.OutOfTheLibary);



        }

        [TestMethod]
        public void CancelBook_TheReaderCannotCanceldBook_ThrowInvalidOperationException()
        {

            //Arrange
            Reader adultReader = new Reader("itay", "Monis", Reader.ReaderType.Adult); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(adultReader); //Create a libary account for a child reader
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.OutOfTheLibaryAndReserved);
           
         

            Assert.ThrowsException<InvalidOperationException>(() => adultLibaryAccount.CancelBook(adultBook));
            Assert.IsFalse(adultLibaryAccount.ReservedBooks.Contains(adultBook));

        }


        #endregion
    }
}
