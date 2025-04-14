using Documents.Infrastructure.Domain;

namespace Documents.WebApi.Utils;
public static class SetValuesNewDocument
{
    public static Document SetValues(IFormFile file, DateTime date)
    {
        Document document = new();
        document.FileName = file.FileName;
        document.FileType = file.Headers.ContentType;
        document.Size = file.Length;
        document.UploadDate = DateTime.Now;
        document.ExpirateDate = date;

        return document;
    }
}