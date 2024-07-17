using System.ComponentModel.DataAnnotations;

namespace Vangoph.Reqeusts;

public class ContactRequest
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [Phone]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Description { get; set; }
}