using NSubstitute;

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
        var dataRepository = new DataRepository(projectDir + "/data.json");

        var data = dataRepository.GetData();

        Assert.That(data.data?.contents?[0], Is.EqualTo(
            new Content(false, false, "Deep Sax 1.mp3", "MS On-Demand H.264", "01:02:53", "828c3395-0303-45cd-9651-bd7226b3198a", false, "test video 23", false, false, 4, "15fa3bc1-760d-45bb-9eeb-1508d3a9a81d", "GgNXuUOo0n1d", 11, 1, false, DateTime.Parse("2021-04-14T21:01:14.533"), DateTime.Parse("2021-04-14T21:05:42.81"), DateTime.Parse("2021-04-14T21:01:14.533"), 1, "https://picsum.photos/seed/picsum/600/600")));
    }

    [Test]
    public void DeleteElementWithGivenId()
    {
        var projectDir = TestUtil.RootProjectDir();
        var dataRepository = new DataRepository(projectDir + "/data.json");
        var id = "0ORe4ifsYSxp";

        var dataBefore = dataRepository.GetData()?.data?.contents;
        dataRepository.DeleteById(id);
        var dataAfter = dataRepository.GetData()?.data?.contents;

        Assert.That(dataBefore, Has.Exactly(1).Matches<Content>(content => content.contentId == id)); 
        Assert.That(dataAfter, Has.Exactly(0).Matches<Content>(content => content.contentId == id)); 
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
