using DataAccessLayer;
using DataAccessLayer.BusinessObject;
using DataAccessLayer.Repository;
using Service.DTO;

namespace Service;

public class ProductService(BaseRepository<Product, ProductDBContext> productRepo)
{
    private readonly BaseRepository<Product, ProductDBContext> productRepo = productRepo;
    public async Task<ResponseEntity<IEnumerable<ProductResponse>>> GetAll()
    {
        try
        {
            var result = await productRepo.GetAllAsync();
            return ResponseEntity<IEnumerable<ProductResponse>>.CreateSuccess(result.Select(entity =>
            {
                return new ProductResponse()
                {
                    ProductId = entity.ProductId,
                    ProductName = entity.ProductName,
                    UnitPrice = entity.UnitPrice,
                    UnitsInStock = entity.UnitsInStock,
                    CategoryId = entity.CategoryId
                };
            }));
        }
        catch (Exception ex)
        {
            return ResponseEntity<IEnumerable<ProductResponse>>.InternalServerError(ex.Message);
        }

    }

    public async Task<ResponseEntity<ProductResponse>> GetByIdAsync(int id)
    {
        try
        {
            var product = await productRepo.GetByIdAsync(id);
            var productResponse = new ProductResponse()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                CategoryId = product.CategoryId
            };

            return ResponseEntity<ProductResponse>.CreateSuccess(productResponse);
        }
        catch (KeyNotFoundException ex)
        {
            return ResponseEntity<ProductResponse>.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return ResponseEntity<ProductResponse>.InternalServerError(ex.Message);
        }
    }
    public async Task<ResponseEntity<ProductResponse>> AddAsync(ProductRequest productRequest)
    {
        try
        {
            var product = new Product()
            {
                ProductName = productRequest.ProductName,
                UnitPrice = productRequest.UnitPrice,
                UnitsInStock = productRequest.UnitsInStock,
                CategoryId = productRequest.CategoryId
            };

            await productRepo.AddAsync(product);
            var productResponse = new ProductResponse()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                CategoryId = product.CategoryId
            };

            return ResponseEntity<ProductResponse>.CreateSuccess(productResponse);
        }
        catch (Exception ex)
        {
            return ResponseEntity<ProductResponse>.InternalServerError(ex.Message);
        }
    }

    public async Task<ResponseEntity<ProductResponse>> UpdateAsync(int id, ProductRequest productRequest)
    {
        try
        {
            var product = await productRepo.GetByIdAsync(id);
            product.ProductName = productRequest.ProductName;
            product.UnitPrice = productRequest.UnitPrice;
            product.UnitsInStock = productRequest.UnitsInStock;
            product.CategoryId = productRequest.CategoryId;
            await productRepo.UpdateAsync(product);
            var productResponse = new ProductResponse()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                CategoryId = product.CategoryId
            };

            return ResponseEntity<ProductResponse>.CreateSuccess(productResponse);
        }
        catch (KeyNotFoundException ex)
        {
            return ResponseEntity<ProductResponse>.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return ResponseEntity<ProductResponse>.InternalServerError(ex.Message);
        }
    }
    public async Task<ResponseEntity<object>> DeleteAsync(int id)
    {
        try
        {
            await productRepo.DeleteAsync(id);
            return ResponseEntity<object>.CreateSuccess(true);
        }
        catch (KeyNotFoundException ex)
        {
            return ResponseEntity<object>.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return ResponseEntity<object>.InternalServerError(ex.Message);
        }
    }

}