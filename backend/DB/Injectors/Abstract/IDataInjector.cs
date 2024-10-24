using MySql.Data;
using MySql.Data.MySqlClient;

namespace Db;


public interface IDataInjector
{
    int InjectData(MySqlConnection connection);
}