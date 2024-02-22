using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OMSSample.Models;

[Table("NewOrderSingle")]
public class OrderSingle
{
    
    [Column("ClOrdId")]
    [Display(Name = "ClOrdId")]
    [MaxLength(500)]
    [Key]
    public string ClOrdId { get; set; }
    
    [Column("Symbol")]
    [Display(Name = "Symbol")]
    [MaxLength(255)]
    public string Symbol { get; set; }
    
    [Column("Price")]
    [Display(Name = "Price")]
    public decimal Price { get; set; }
    
    [Column("Side")]
    [Display(Name = "Side")]
    public char Side { get; set; }
    
    [Column("TransactTime")]
    [Display(Name = "TransactTime")]
    public DateTime TransactTime { get; set; }
    
    [Column("OrdType")]
    [Display(Name = "OrdType")]
    public char OrdType { get; set; }
    
    [Column("OrdQty")]
    [Display(Name = "OrdQty")]
    public uint OrdQty { get; set; }
}