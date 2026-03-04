using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ignatovich.Domain.Entities;

[Table("Authors")]
public class Author
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NormalizedName { get; set; }

    // Навигационное свойство для связи с книгами
    public virtual ICollection<Book> Books { get; set; }

    //public Author()
    //{
    //    Books = new HashSet<Book>();
    //}

   
}