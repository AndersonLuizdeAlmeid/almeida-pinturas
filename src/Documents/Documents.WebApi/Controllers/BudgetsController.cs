using Documents.Application.Services;
using Documents.Infrastructure.Domain;
using Documents.WebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Documents.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class BudgetsController(IBudgetService _budgetService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _budgetService.GetAllBudgetsAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var budget = await _budgetService.GetBudgetByIdAsync(id);
        return budget is null ? NotFound() : Ok(budget);
    }

    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm]Budget budget)
    {
        budget.Content = await ConvertIFormFileToByte.ConvertToBytesAsync(file);

        if (budget.Content == null)
            return BadRequest();

        budget.Id = Guid.NewGuid().ToString();
        var result = await _budgetService.CreateBudgetAsync(budget);
        if (result)
            return CreatedAtAction(nameof(GetById), new { id = budget.Id }, budget);

        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
        => await _budgetService.DeleteBudgetAsync(id) ? NoContent() : NotFound();
}