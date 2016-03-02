using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Library.Objects
{
  public class AuthorTest : IDisposable
  {
    public void AuthorTestDB()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_AuthorsEmptyAtFirst()
    {
      int result = Author.GetAll().Count;

      Assert.Equal(0, result);
    }
//
    [Fact]
    public void Test_EqualOverride_TrueForSameName()
    {
      var authorOne = new Author("Mark Twain");
      var authorTwo = new Author("Mark Twain");

      Assert.Equal(authorOne, authorTwo);
    }
//
    [Fact]
    public void Test_Save_SavesAuthorDataBase()
    {
      var testAuthor = new Author("Johnny");
      testAuthor.Save();

      var testList = new List<Author>{testAuthor};
      var result = Author.GetAll();

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_SaveAssignsIdToOBjects()
    {
      var testAuthor = new Author("Mark Twain");
      testAuthor.Save();

      var savedAuthor = Author.GetAll()[0];

      int result = savedAuthor.GetId();
      int testId = testAuthor.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_FindAuthorInDataBase()
    {
      var testAuthor = new Author("Mark Twain");
      testAuthor.Save();

      var foundAuthor = Author.Find(testAuthor.GetId());

      Assert.Equal(testAuthor, foundAuthor);
    }

    [Fact]
    public void Test_Update_UpdatesAuthorInDataBase()
    {
      var authorNameOne = "Mark Twain";
      var testAuthor = new Author(authorNameOne);
      testAuthor.Save();

      var authorNameTwo = "Dalton Trumbo";
      testAuthor.Update(authorNameTwo);

      Author result = new Author(testAuthor.GetName());

      Assert.Equal(result.GetName(), authorNameTwo);
    }
//
    [Fact]
    public void Test_Delete_DeletesAuthorFromDatabase()
    {

      string nameOne = "Huckleberry Finn";
      Book testBook = new Book(nameOne);
      testBook.Save();

      Author testAuthor1 = new Author("Mark Twain");
      testAuthor1.Save();
      Author testAuthor2 = new Author("Dalton Trumbo");
      testAuthor2.Save();

      testBook.AddAuthor(testAuthor1);
      testBook.AddAuthor(testAuthor2);

      testAuthor2.Delete();


      List<Author> resultAuthors = testBook.GetAuthors();
      List<Author> testAuthorList = new List<Author> {testAuthor1};

      List<Author> authorsList = Author.GetAll();

      Assert.Equal(testAuthorList, authorsList);
      Assert.Equal(testAuthorList, resultAuthors);
    }

    [Fact]
    public void Test_AddBook_AddsBookToAuthor()
    {
      //Arrange
      Author testAuthor = new Author("Mark Twain");
      testAuthor.Save();

      Book testBook = new Book("Tom Sawyer");
      testBook.Save();

      //Act
      testAuthor.AddBook(testBook);

      List<Book> result = testAuthor.GetBooks();
      List<Book> testList = new List<Book>{testBook};

      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_GetBook_ReturnsAllAuthorBooks()
    {
      //Arrange
      Author testAuthor = new Author("Mark Twain");
      testAuthor.Save();

      Book testBook1 = new Book("Tom Sawyer");
      testBook1.Save();

      Book testBook2 = new Book("Huckleberry Finn");
      testBook2.Save();

      testAuthor.AddBook(testBook1);
      testAuthor.AddBook(testBook2);


      List<Book> resultList = testAuthor.GetBooks();
      List<Book> compareList = new List<Book> {testBook1, testBook2};

      //Assert
      Assert.Equal(resultList, compareList);
    }
//
    public void Dispose()
    {
      Book.DeleteAll();
      Author.DeleteAll();
    }
  }
}
