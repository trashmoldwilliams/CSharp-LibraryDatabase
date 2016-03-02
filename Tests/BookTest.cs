using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Library.Objects
{
  public class BookTest : IDisposable
  {
    public BookTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_BooksEmptyAtFirst()
    {
      int result = Book.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      var BookOne = new Book("Treasure Island");
      var BookTwo = new Book("Treasure Island");

      Assert.Equal(BookOne, BookTwo);
    }

    [Fact]
    public void Test_Save_SavesBookToDataBase()
    {
      var testBook = new Book("Alice in Wonderland");
      testBook.Save();

      var result = Book.GetAll();
      var testList = new List<Book>{testBook};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToBookObject()
    {
      var testBook = new Book("Choke");
      testBook.Save();

      var savedBook = Book.GetAll()[0];

      int result = savedBook.GetId();
      int testId = testBook.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsBookInDataBase()
    {
      var testBook = new Book("Edward Scissorhands");
      testBook.Save();

      var foundBook = Book.Find(testBook.GetId());

      Assert.Equal(testBook, foundBook);
    }

    [Fact]
    public void Test_GetAuthors_RetrieveAllAuthorsWithBooks()
    {
      var testBook = new Book("The Stranger");
      testBook.Save();

      var authorOne = new Author("Johnathon");
      authorOne.Save();

      var authorTwo = new Author("Bobbie");
      authorTwo.Save();

      testBook.AddAuthor(authorOne);
      testBook.AddAuthor(authorTwo);

      var testAuthorList = new List<Author> {authorOne, authorTwo};
      var resultAuthorList = testBook.GetAuthors();

      Assert.Equal(testAuthorList, resultAuthorList);
    }

    [Fact]
    public void Test_Update_UpdatesBookInDataBase()
    {
      string name = "Roger";
      var testBook = new Book(name);
      testBook.Save();

      string newName = "Snoop Dog";
      testBook.Update(newName);
      string result = testBook.GetName();

      Assert.Equal(newName, result);
    }
    //
    [Fact]
    public void Test_Delete_DeletesBooksFromDataBase()
    {
      string nameOne = "Ice Cube";
      var testBookOne = new Book(nameOne);
      testBookOne.Save();

      string nameTwo = "Marshal Mathers";
      var testBookTwo = new Book(nameTwo);
      testBookTwo.Save();

      testBookOne.Delete();
      var resultBooks = Book.GetAll();
      var testBookList = new List<Book> {testBookTwo};

      Assert.Equal(testBookList, resultBooks);

    }
    [Fact]
    public void Test_AddAuthor_AddAuthorToBook()
    {
      Book testBook = new Book("Harry Styles");
      testBook.Save();

      Author testAuthor = new Author("Zayne");
      testAuthor.Save();

      Author testAuthor2 = new Author("Zack and Cody");
      testAuthor2.Save();

      testBook.AddAuthor(testAuthor);
      testBook.AddAuthor(testAuthor2);

      List<Author> result = testBook.GetAuthors();
      List<Author> testList = new List<Author> {testAuthor, testAuthor2};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_GetAuthors_ReturnsAllBooksAuthor()
    {
      Book testBook = new Book("Tim");
      testBook.Save();

      Author testAuthor1 = new Author("Andrew");
      testAuthor1.Save();

      Author testAuthor2 = new Author("Thea");
      testAuthor2.Save();

      testBook.AddAuthor(testAuthor1);
      testBook.AddAuthor(testAuthor2);
      List<Author> savedAuthors = testBook.GetAuthors();
      List<Author> testList = new List<Author> {testAuthor1, testAuthor2};

      Assert.Equal(testList, savedAuthors);

    }

    //Leave at bottom of test
    public void Dispose()
    {
      Book.DeleteAll();
      Author.DeleteAll();
    }
  }
}
