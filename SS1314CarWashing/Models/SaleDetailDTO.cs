namespace SS1314CarWashing.Models;

public class SaleDetailDTO
{
    public Guid SaleDetailId { get; set; }
    public Guid SaleId { get; set; }
    public string ItemName { get; set; }
    public double Price { get; set; }
    public int Qty { get; set; }
    public double Amount { get; set; }
}
