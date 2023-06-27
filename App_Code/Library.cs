using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.IO;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using System.Web.Hosting;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

/// <summary>
/// Summary description for Library
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Library : System.Web.Services.WebService
{
    string path = Path.Combine(HostingEnvironment.ApplicationPhysicalPath,
    "App_Data", "Books.xml");
    Catalog catalog = new Catalog();

    public Library()
    {
        XDocument xdoc = XDocument.Load(path);
        XElement Catalog = xdoc.Element("catalog");
        if (Catalog != null)
        {
            foreach (XElement Book in Catalog.Elements("book"))
            {
                Book book = new Book();
                XAttribute id = Book.Attribute("id");
                book.id = id.Value;
                XElement author = Book.Element("author");
                book.author = author.Value;
                XElement title = Book.Element("title");
                book.title = title.Value;
                XElement genre = Book.Element("genre");
                book.genre = genre.Value;
                XElement price = Book.Element("price");
                book.price = price.Value;
                XElement publish_date = Book.Element("publish_date");
                book.publish_date = publish_date.Value;
                XElement description = Book.Element("description");
                book.description = description.Value;
                catalog.catalog.Add(book);
            }
        }
    }

    [WebMethod]
    public Catalog GetAllBooks()
    {
        return catalog;
    }

    [WebMethod]
    public Catalog GetByID(string ID)
    {
        foreach (var book in catalog.catalog)
        {
            if (book.id.Equals(ID))
            {
                var cat = new Catalog();
                cat.catalog.Add(book);
                return cat;
            }
        }
        return new Catalog();
    }

    [WebMethod]
    public Catalog GetByTitle(string title)
    {
        Catalog books = new Catalog();
        foreach (var book in catalog.catalog)
        {
            if (book.title.Equals(title))
                books.catalog.Add(book);
        }
        return books;
    }

    [WebMethod]
    public void PostBook(Book book)
    {
        XDocument document = XDocument.Load(path);
        XElement catalogElement = document.Element("catalog");
        if (catalogElement != null)
        {
            XElement bookElement = new XElement("book");
            bookElement.SetAttributeValue("id", book.id);
            bookElement.Add("author", book.author);
            bookElement.Add("title", book.title);
            bookElement.Add("genre", book.genre);
            bookElement.Add("price", book.price);
            bookElement.Add("publish_date", book.publish_date);
            bookElement.Add("description", book.description);
            catalogElement.Add(bookElement);
            document.Save(path);
        }
    }

    [WebMethod]
    public void DeleteBook(string id) 
    {
        XDocument document = XDocument.Load(path);
        XElement book = document.Descendants("book")
            .FirstOrDefault(e => e.Attribute("id").Value == id);
        if (book != null)
        {
            book.Remove();
            document.Save(path);
        }
    }
}
