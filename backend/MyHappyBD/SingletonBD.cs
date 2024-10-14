namespace backend.MyHappyBD;

public class SingletonBD
{
    private static SingletonBD _instance = null;
    private static readonly object _lock = new object();

    public static SingletonBD Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new SingletonBD();
                }
                return _instance;
            }
        }
    }

    private SingletonBD()
    {
        
    }

    public void DoSomething()
    {
        Console.WriteLine("Doing something...");
    }
}