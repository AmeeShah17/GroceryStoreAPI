using System;
using System.IO;
using Microsoft.AspNetCore.Http;

public static class ImageHelper
{
    public static string ConvertImageToBase64(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
            return null;

        using (var memoryStream = new MemoryStream())
        {
            imageFile.CopyTo(memoryStream);
            byte[] imageBytes = memoryStream.ToArray();
            return Convert.ToBase64String(imageBytes);
        }
    }
}
