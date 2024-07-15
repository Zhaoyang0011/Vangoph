using System;
using System.ComponentModel.DataAnnotations;

namespace Vangoph.Models;

public class Image
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10000")]
    public double Price { get; set; }
    
    public DateTime CreationDate { get; set; }
    public string ImageUrl { get; set; }
    public int? AuthorId { get; set; }
}