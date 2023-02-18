using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SS1314CarWashing.Models;

public class SaleDTO
{

    [Key]
    public Guid SaleId { get; set; }
    [ForeignKey("Customer")]
    [Display(Name = "Customer Name")]
    public string CustomerName { get; set; }
    [DataType(DataType.Date)]
    [Display(Name = "Issue Date")]
    public DateTime IssueDate { get; set; }
    [Display(Name = "Invoice Number")]
    public string InvoiceNumber { get; set; }
    public double Total { get; set; }
    public int Discount { get; set; }
    public double GrandTotal { get; set; }

    public Customer Customer { get; set; }
    public List<SaleDetailDTO> SaleDetails { get; set; }
}
