using Newtonsoft.Json.Linq;

namespace MinimalApi;

public record Content(bool liveStatusOnAir, bool liveStatusRecording, string? onDemandFileName, string? onDemandEncodingDescription, string? onDemandDuration, string? gidEncodingProfileOnDemand, bool liveMultibitrate, string? title, bool trash, bool hasPoster, int onDemandEncodingStatus, string? gidProperty, string? contentId, int contentType, int deliveryStatus, bool protectedEmbed, DateTime creationDate, DateTime updateDate, DateTime publishDateUTC, int publishStatus, string? imageUrl);

public record ContentCount(int totalContents, int onDemandVideoCount, int onDemandAudioCount, int liveEventVideoCount, int liveEventAudioCount, int playlistCount, int originCount, int folderCount);

public record Data(List<string>? folders, List<Content>? contents, int returnedContents, int returnedFolders, ContentCount? contentCount);

public record DataRoot(int code, string? status, bool isSuccessStatusCode, Data? data);

interface IDataRepository
{
    public DataRoot GetData();
}

class DataRepository : IDataRepository
{
    private String filePath;
    private DataRoot data;

    public DataRepository(String filePath)
    {
        this.filePath = filePath;
        this.data = JObject.Parse(ReadData()).ToObject<DataRoot>()!;
    }

    public DataRoot GetData()
    {
        return this.data;
    }

    private String ReadData()
    {
        String output = "";
        String? line;
        Boolean isFirstIteration = true;
        StreamReader sr = new StreamReader(filePath);
        line = sr.ReadLine();
        while (line != null)
        {
            if (isFirstIteration)
            {
                output += line;
                isFirstIteration = false;
            }
            else
            {
                output += Environment.NewLine + line;
            }
            line = sr.ReadLine();
        }
        sr.Close();
        return output;
    }

    public void DeleteById(String id)
    {
    }
}
