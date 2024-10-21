using System.Text.Json;
using backend.MyHappyBD;
using Entities;

namespace Db;

public class SessionDAO : IDAO<Session>
{
    private const string FilePath = "session.json";

    public int Create(Session element)
    {
        var sessions = ReadAll();
        sessions.Add(element);
        JsonManager.WriteJsonAsync(FilePath, sessions).Wait(); 
        return 1; 
    }

    public Session? Read(Guid id)
    {
        var sessions = ReadAll(); 
        return sessions.FirstOrDefault(s => s.SessionId == id);
    }

    public List<Session> ReadAll()
    {
        return JsonManager.ReadJsonAsync<List<Session>>(FilePath).Result ?? new List<Session>(); 
    }

    public int Update(Session element)
    {
        var sessions = ReadAll(); 
        var index = sessions.FindIndex(s => s.SessionId == element.SessionId); // Busca la sesión a actualizar

        if (index == -1)
        {
            return 0; 
        }

        sessions[index] = element; 
        JsonManager.WriteJsonAsync(FilePath, sessions).Wait();
        return 1; 
    }

    public bool Delete(Guid id)
    {
        var sessions = ReadAll(); 
        var sessionToRemove = sessions.FirstOrDefault(s => s.SessionId == id); 

        if (sessionToRemove == null)
        {
            return false; 
        }

        sessions.Remove(sessionToRemove);
        JsonManager.WriteJsonAsync(FilePath, sessions).Wait();
        return true; 
    }
}
