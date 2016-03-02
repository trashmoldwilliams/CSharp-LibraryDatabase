using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Library.Objects

{
  public class Book
  {
    private int _id;
    private string _book_name;

    public Book(string bookName, int Id = 0)
    {
      _id = Id;
      _book_name = bookName;
    }

    public override bool Equals(System.Object otherBook)
    {
      if(!(otherBook is Book))
      {
        return false;
      }
      else
      {
        var newBook = (Book) otherBook;
        bool idEquality = this.GetId() == newBook.GetId();
        bool nameEquality = this.GetName() == newBook.GetName();

        return (idEquality && nameEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
    return _book_name;
    }

    public static List<Book> GetAll()
    {
      var allBooks = new List<Book>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      var cmd = new SqlCommand("SELECT * FROM books;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {

        var BookId = rdr.GetInt32(0);
        var BookName = rdr.GetString(1);

        var newBook = new Book(BookName, BookId);
        allBooks.Add(newBook);

      }

      if(rdr != null)
      {
        rdr.Close();
      }

      if(conn != null)
      {
        conn.Close();
      }

      return allBooks;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      var cmd = new SqlCommand("INSERT INTO books (title) OUTPUT INSERTED.id VALUES (@BookName);", conn);
      var nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@BookName";
      nameParameter.Value = this.GetName();

      cmd.Parameters.Add(nameParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }

      if(rdr != null)
      {
        rdr.Close();
      }

      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Book Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      var cmd = new SqlCommand("SELECT * FROM books WHERE id = @BookId;", conn);
      var bookIdParameter = new SqlParameter();
      bookIdParameter.ParameterName = "@BookId";
      bookIdParameter.Value = id;
      cmd.Parameters.Add(bookIdParameter);
      rdr = cmd.ExecuteReader();

      int foundBookId = 0;
      string foundBookName = null;

      while(rdr.Read())
      {
        foundBookId = rdr.GetInt32(0);
        foundBookName = rdr.GetString(1);
      }

      var foundBook = new Book(foundBookName, foundBookId);

      if(rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }

      return foundBook;
    }

    public void AddAuthor(Author newAuthor)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO books_authors (book_id, author_id) VALUES (@BookId, @AuthorId)", conn);
      SqlParameter bookIdParameter = new SqlParameter();
      bookIdParameter.ParameterName = "@BookId";
      bookIdParameter.Value = this.GetId();
      cmd.Parameters.Add(bookIdParameter);

      SqlParameter authorIdParameter = new SqlParameter();
      authorIdParameter.ParameterName = "@AuthorId";
      authorIdParameter.Value = newAuthor.GetId();
      cmd.Parameters.Add(authorIdParameter);

      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }

    public List<Author> GetAuthors()
     {
       SqlConnection conn = DB.Connection();
       SqlDataReader rdr = null;
       conn.Open();

       SqlCommand cmd = new SqlCommand("SELECT author_id FROM books_authors WHERE book_id = @BookId;", conn);
       SqlParameter bookIdParameter = new SqlParameter();
       bookIdParameter.ParameterName = "@BookId";
       bookIdParameter.Value = this.GetId();
       cmd.Parameters.Add(bookIdParameter);

       rdr = cmd.ExecuteReader();

       List<int>authorsIds = new List<int> {};
       while(rdr.Read())
       {
         int AuthorId = rdr.GetInt32(0);
         authorsIds.Add(AuthorId);
       }
       if (rdr != null)
       {
         rdr.Close();
       }

       List<Author> authors = new List<Author> {};
       foreach (int AuthorId in authorsIds)
       {
         SqlDataReader queryReader = null;
         SqlCommand authorQuery = new SqlCommand("SELECT * FROM authors WHERE id = @AuthorId;", conn);

         SqlParameter authorIdParameter = new SqlParameter();
         authorIdParameter.ParameterName = "@AuthorId";
         authorIdParameter.Value = AuthorId;
         authorQuery.Parameters.Add(authorIdParameter);

         queryReader = authorQuery.ExecuteReader();
         while(queryReader.Read())
         {
               int thisAuthorId = queryReader.GetInt32(0);
               string authorDescription = queryReader.GetString(1);
               Author foundAuthor = new Author(authorDescription, thisAuthorId);
               authors.Add(foundAuthor);
         }
         if (queryReader != null)
         {
           queryReader.Close();
         }
       }
       if (conn != null)
       {
         conn.Close();
       }
       return authors;
     }

     public void Update(string newBookName)
     {
       SqlConnection conn = DB.Connection();
       SqlDataReader rdr;
       conn.Open();

       var cmd = new SqlCommand("UPDATE books SET title = @NewBookName OUTPUT INSERTED.title WHERE id = @BookId;", conn);

       var newBookIdParameter = new SqlParameter();
       newBookIdParameter.ParameterName = "@NewBookName";
       newBookIdParameter.Value = newBookName;
       cmd.Parameters.Add(newBookIdParameter);

       var bookIdParameter = new SqlParameter();
       bookIdParameter.ParameterName = "@BookId";
       bookIdParameter.Value = this.GetId();
       cmd.Parameters.Add(bookIdParameter);
       rdr = cmd.ExecuteReader();

       while(rdr.Read())
       {
         this._book_name = rdr.GetString(0);
       }

       if(rdr != null)
       {
         rdr.Close();
       }

       if(conn != null)
       {
         conn.Close();
       }
     }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = new SqlCommand("DELETE FROM books WHERE id = @BookId;", conn);

      var bookIdParameter = new SqlParameter();
      bookIdParameter.ParameterName = "@BookId";
      bookIdParameter.Value = this.GetId();

      cmd.Parameters.Add(bookIdParameter);
      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }

    //IDisposable testing | Dispose/Delete method
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM books;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
