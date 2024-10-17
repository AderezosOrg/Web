namespace Db;

public static class ObjectMapper
{
    public static string MapBoolean(bool value)
    {
        return value? "1":"0";
    }

    public static string MapDateTime(DateTime value)
    {
        return value.ToString("yyyy-MM-dd H:mm:ss");
    }
}