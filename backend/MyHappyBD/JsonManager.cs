using System.Text.Json;

namespace backend.MyHappyBD;

public class JsonManager
{
    
    public async Task<T> ReadJsonAsync<T>(string filePath)
    {
        try
        {
            string jsonString = await File.ReadAllTextAsync(filePath);
            T obj = JsonSerializer.Deserialize<T>(jsonString);
            return obj;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer el archivo JSON: {ex.Message}");
            return default;
        }
    }

    public async Task WriteJsonAsync<T>(string filePath, T obj)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al escribir en el archivo JSON: {ex.Message}");
        }
    }
}
    
