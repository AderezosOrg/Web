namespace Db;

public static class ObjectMapper
{
    public static string MapBoolean(bool value)
    {
        return value? "1":"0";
    }
}