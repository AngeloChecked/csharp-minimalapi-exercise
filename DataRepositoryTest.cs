namespace MinimalApi;

public class DataRepositoryTests
{

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ReadAFileInTheRootOfProject()
    {
        var projectDir = TestUtil.RootProjectDir();
        var dataRepository = new DataRepository(projectDir + "/testfile.txt");

        var data = dataRepository.GetData();

        string expected =
            "hello world" + Environment.NewLine +
            "dummy data" + Environment.NewLine +
            "lalala";
        Assert.That(data, Is.EqualTo(expected));
    }
}

public class TestUtil
{
    public static String? RootProjectDir()
    {
        String workingDirectory = Environment.CurrentDirectory;
        String? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName;
        return projectDirectory;
    }
}
