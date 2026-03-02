using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ignatovich.Domain.Entities;

[Table("Books")]
public class Book
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public string? Image { get; set; }

    [Required]
    [Range(0, 5)]
    [Column(TypeName = "decimal(3,2)")]
    public decimal Rating { get; set; } // Обязательное свойство - рейтинг книги

    // Навигационное свойство для связи с автором
    [ForeignKey(nameof(AuthorId))]
    public virtual Author Author { get; set; }
}