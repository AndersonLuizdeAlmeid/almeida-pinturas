namespace Documents.WebApi.Utils;
public static class ConvertIFormFileToByte
{
    public static async Task<byte[]> ConvertToBytesAsync(IFormFile file)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        return ms.ToArray();
    }
}