using Documents.Infrastructure.Domain;
using Documents.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Documents.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentsController(IDocumentService _documentService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _documentService.GetAllDocumentsAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var document = await _documentService.GetDocumentByIdAsync(id);
        return document is null ? NotFound() : Ok(document);
    }

    [HttpPost]
    public async Task<IActionResult> Upload([FromBody] Document document)
    {
        var result = await _documentService.CreateDocumentAsync(document);
        if (result)
            return CreatedAtAction(nameof(GetById), new { id = document.Id }, document);

        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
        => await _documentService.DeleteDocumentAsync(id) ? NoContent() : NotFound();
}