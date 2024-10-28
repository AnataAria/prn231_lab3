using DataAccessLayer;
using DataAccessLayer.BusinessObject;
using DataAccessLayer.Repository;
using Microsoft.Identity.Client;
using Service.DTO;

namespace Service;

public class CategoryService(BaseRepository<Category, ProductDBContext> repository)
{
    private readonly BaseRepository<Category, ProductDBContext> repository = repository;

    public async Task<ResponseEntity<ICollection<CategoryResponse>>> GetAllCategory()
    {
        try
        {
            var result = await repository.GetAllAsync();
            return ResponseEntity<ICollection<CategoryResponse>>.CreateSuccess(result.Select(entity =>
            {
                return new CategoryResponse()
                {
                    CategoryId = entity.CategoryId,
                    CategoryName = entity.CategoryName
                };
            }).ToList());
        }
        catch (Exception ex)
        {
            return ResponseEntity<ICollection<CategoryResponse>>.InternalServerError(ex.Message);
        }
    }

}