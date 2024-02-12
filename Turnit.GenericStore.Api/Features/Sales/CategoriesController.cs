using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using Turnit.GenericStore.Domain.Entity;
using Turnit.GenericStore.Domain.Interface.Service;

namespace Turnit.GenericStore.Api.Features.Sales;

[Route("[controller]")]
public class CategoriesController : ApiControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet, Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<Category>>> AllCategories()
    {
        var result = await _categoryService.GetAllCategories();
        if(result.Count == 0)
        {
            return NotFound();
        }
        
        return Ok(result);
    }
}