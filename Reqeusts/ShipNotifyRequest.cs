using System.ComponentModel.DataAnnotations;

namespace Vangoph.Reqeusts;

public class ShipNotifyRequest
{
    [Required(ErrorMessage = "TrackingID is required")]
    public string TrackingID { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Item number is required")]
    public string Item { get; set; }

    public string Description { get; set; }
}