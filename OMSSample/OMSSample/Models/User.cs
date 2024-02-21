using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace OMSSample.Models;

[Table("User")]
public class User
{
    [Column("id")]
    [Display(Name = "id")]
    public int Id { get; set; }
    
    [Column("username")]
    [Display(Name = "username")]
    [Required]
    [NotNull]
    public string? Username { get; set; }
    
    [Column("password")]
    [Display(Name = "password")]
    [NotNull]
    public string? Password { get; set; }

    public List<OrderSingle> NewOrderSingle { get; set; } = new List<OrderSingle>();
}