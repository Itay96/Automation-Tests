//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
//using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Libary.UnitTest
{

    [TestFixture]
    public class LibaryNUnitTests
    { 
        [Test]

        public void OrderBook_childTryToOrderAdultBook_ThrowInvalidOperationException()
        {

            //Arrange
            Reader childReader = new Reader("itay", "Monis", Reader.ReaderType.Child); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(childReader); //Create a libary account for a child reader
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.InTheLibary);


            //Assert&&Act

            Assert.Throws<InvalidOperationException>(() => adultLibaryAccount.OrderBook(adultBook));
            Assert.IsFalse(adultLibaryAccount.ReservedBooks.Contains(adultBook));
            Assert.IsTrue(adultBook.Status == Book.BookStatus.InTheLibary);

        }



        [Test]
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



        [Test]
        public void OrderBook_adultTryToOrderChildBook_ThrowInvalidOperationException()
        {

            //Arrange
            Reader adultReader = new Reader("itay", "Monis", Reader.ReaderType.Adult); //Initialize a child reader
            LibaryAccount adultLibaryAccount = new LibaryAccount(adultReader); //Create a libary account for a child reader
            Book childBook = new Book(Book.BookType.ChildrenBook, Book.BookStatus.InTheLibary);


            //Assert&&Act

            Assert.Throws<InvalidOperationException>(() => adultLibaryAccount.OrderBook(childBook));
            Assert.IsFalse(adultLibaryAccount.ReservedBooks.Contains(childBook));
            Assert.IsTrue(childBook.Status == Book.BookStatus.InTheLibary);


        }




    }
}
