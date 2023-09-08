
namespace MinimalApi;

class MinimalApi
{
    public static void Main()
    {
        String workingDirectory = Environment.CurrentDirectory;
        Console.WriteLine($"workingDirectory -> {workingDirectory}");

        var dataRepository = new DataRepository("data.json");
        var webserver = new WebServer(dataRepository);
        webserver.Run();
    }
}
