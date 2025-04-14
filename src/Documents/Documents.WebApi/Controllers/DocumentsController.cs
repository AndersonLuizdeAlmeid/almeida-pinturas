using Documents.Infrastructure.Domain;
using Documents.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Documents.WebApi.Utils;

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

    [HttpGet("userId/{userId}")]
    public async Task<IActionResult> GetByUserId(long userId)
    {
        var document = await _documentService.GetDocumentByUserIdAsync(userId);
        return document is null ? NotFound() : Ok(document);
    }

    [HttpPost("user/{userId}/{date}")]
    public async Task<IActionResult> Upload([FromForm] IFormFile file, long userId, DateTime date)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Arquivo inválido.");

        var document = SetValuesNewDocument.SetValues(file, date);
        document.Content = await ConvertIFormFileToByte.ConvertToBytesAsync(file);

        if (document.Content == null)
            return BadRequest();

        var result = await _documentService.CreateDocumentAsync(document, userId);
        if (result)
            return CreatedAtAction(nameof(GetById), new { id = document.Id }, document);

        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
        => await _documentService.DeleteDocumentAsync(id) ? NoContent() : NotFound();
}