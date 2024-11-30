using AxxesMarket.Api.Mapping;
using AxxesMarket.Api.Persistence;
using AxxesMarket.Shared.WebModels;
using AxxesMarket.Shared.WebModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AxxesMarket.Api.Controllers;

public class ProductsController : BaseApiController
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ProductsController(ILogger<ProductsController> logger, IUnitOfWork unitOfWork)  
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var request = HttpContext.Request;
        var products = await _unitOfWork.ProductRepository.GetAllProductsAsync();
        return Ok(new JsonResponse<IEnumerable<ProductResponse>> { Result = products.Select(x => x.MapToProductResponse()) });
    }

    [HttpGet("my")]
    [Authorize]
    public async Task<IActionResult> GetMyProducts()
    {
        var products = await _unitOfWork.ProductRepository.GetMyProductsAsync();
        return Ok(new JsonResponse<IEnumerable<ProductResponse>> { Result = products.Select(x => x.MapToProductResponse()) });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await _unitOfWork.ProductRepository.GetProductByIdAsync(id);
        if(product is null)
        {
            return NotFound(new JsonResponse { Errors = new List<string> { $"Product with id {id} could not be found" } });
        } else
        {
            return Ok(new JsonResponse<ProductDetailResponse?> { Result = product.MapToDetailProductResponse() });
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateProduct([FromBody] CreateEditProductRequest request)
    {
        var newId = _unitOfWork.ProductRepository.CreateProduct(request.MapToProduct());
        await _unitOfWork.SaveChangesAsync();

        return Ok(new JsonResponse<Guid> { Result = newId });
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> CreatProduct(Guid id, [FromBody] CreateEditProductRequest request)
    {
        await _unitOfWork.ProductRepository.UpdateProduct(request.MapToProduct());
        await _unitOfWork.SaveChangesAsync();

        return Ok(new JsonResponse<Guid> { Result = id });
    }

    [HttpPut("{id}/buy")]
    [Authorize]
    public async Task<IActionResult> BuyProductAsync(Guid id)
    {
        await _unitOfWork.ProductRepository.BuyProduct(id);
        return Ok(new JsonResponse<Guid> { Result = id });
    }
}
