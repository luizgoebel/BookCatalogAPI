using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Api.DTOs;

public class BookCreationDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    [Range(1500, 2050)]
    public int PublicationYear { get; set; }
}
