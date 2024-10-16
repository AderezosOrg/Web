using MySql.Data;
using MySql.Data.MySqlClient;

namespace Db;

public abstract class DataInjector : IDataInjector
{
    private protected string? _injectionCommand;

    public int InjectData(MySqlConnection connection)
    {
        int injectionResult = 0;

        try
        {
            MySqlCommand injectionCommand = new MySqlCommand(_injectionCommand,connection);
            injectionCommand.ExecuteNonQuery();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            Console.WriteLine(ex);
            injectionResult = ex.Number;
        }
        return injectionResult;
    }
}