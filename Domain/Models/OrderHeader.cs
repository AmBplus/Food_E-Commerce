using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Services.Common.Abstract;

namespace Domain.Models;

public class OrderHeader : BaseModel<int>
{
    public  string PhoneNumber { get; set; }
    public  string Comment { get; set; }
    public  string Name { get; set; }
    [Required]
    public DateTime PickUpTime { get; set; }
    [NotMapped]
    [Display(Name = "Pick Up Time")]
    [Required]
    public DateTime PickUpDate { get; set; }
    [Display(Name = "Pick Up Date")]
    [Required]
    public string UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    [ValidateNever]
    public virtual ApplicationUser ApplicationUser { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
   // public string TransactionId { get; set; }
    [Required]
    [DisplayFormat(DataFormatString = "{0:C}")]
    [Display(Name = "Order Total")]
    public decimal OrderTotal { get; set; }
    public string? SessionId { get; set; }
    public string? PaymentIntentId { get; set; }
}