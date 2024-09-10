using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts.Models;

[Table("user")]
public class User
{
    [Key, Required]
    public int Id { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }
    public string? Region { get; set; }
    public string? District { get; set; }
    public string? Street { get; set; }
}