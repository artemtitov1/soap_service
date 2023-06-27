using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

[DataContract(Name = "catalog")]
[KnownType(typeof(List<Book>))]
public class Catalog
{
    [DataMember]
    public List<Book> catalog = new List<Book>();
}

[DataContract(Name = "book")]
public class Book
{
    [DataMember]
    public string id;
    [DataMember]
    public string author;
    [DataMember]
    public string title;
    [DataMember]
    public string genre;
    [DataMember]
    public string price;
    [DataMember]
    public string publish_date;
    [DataMember]
    public string description;
}