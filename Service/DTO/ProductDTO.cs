namespace Service.DTO;

public class ProductResponse {
    public int ProductId {get; set;}
    public string? ProductName {get; set;}
    public int CategoryId {get; set;}
    public int UnitsInStock {get; set;}
    public decimal UnitPrice {get; set;}
}

public class ProductRequest {
    public string? ProductName {get; set;}
    public int CategoryId {get; set;}
    public int UnitsInStock {get; set;}
    public decimal UnitPrice {get; set;}
}