
namespace NuclearesWebUI.Components
{
    public class Helper
    {
        public static string ConverterImageToBase64(string filepath)
        {
            byte[] imageByte = File.ReadAllBytes(filepath);
            string base64 = Convert.ToBase64String(imageByte);

            string mimeType = Path.GetExtension(filepath).ToLower() switch
            {
                ".png" => "image/png",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                ".svg" => "image/svg+xml",
                _ => "application/octet-stream"
            };

            return $"data:{mimeType};base64,{base64}";
        }
    }
}
