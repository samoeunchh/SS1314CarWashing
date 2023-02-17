using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SS1314CarWashing.Models;

public class Sale
{
    [Key]
    public Guid SaleId { get; set; }
    [ForeignKey("Customer")]
    [Display(Name ="Customer Name")]
    public Guid CustomerId { get; set; }
    [DataType(DataType.Date)]
    [Display(Name = "Issue Date")]
    public DateTime IssueDate { get; set; }
    [Display(Name = "Invoice Number")]
    public string InvoiceNumber { get; set; }
    public double Total { get; set; }
    public int Discount { get; set; }
    public double GrandTotal { get; set; }

    public Customer Customer { get; set; }
    public List<SaleDetail> SaleDetails { get; set; }
}
