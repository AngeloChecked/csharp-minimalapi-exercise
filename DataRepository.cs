using Newtonsoft.Json.Linq;

namespace MinimalApi;

public record Content(bool liveStatusOnAir, bool liveStatusRecording, string? onDemandFileName, string? onDemandEncodingDescription, string? onDemandDuration, string? gidEncodingProfileOnDemand, bool liveMultibitrate, string? title, bool trash, bool hasPoster, int onDemandEncodingStatus, string? gidProperty, string? contentId, int contentType, int deliveryStatus, bool protectedEmbed, DateTime creationDate, DateTime updateDate, DateTime publishDateUTC, int publishStatus, string? imageUrl);

public record ContentCount(int totalContents, int onDemandVideoCount, int onDemandAudioCount, int liveEventVideoCount, int liveEventAudioCount, int playlistCount, int originCount, int folderCount);

public record Data(List<string>? folders, List<Content>? contents, int returnedContents, int returnedFolders, ContentCount? contentCount);

public record DataRoot(int code, string? status, bool isSuccessStatusCode, Data? data);

interface IDataRepository
{
    public DataRoot GetData();
    public void DeleteById(String id);
    public DataRoot SearchByTitle(String title);
}

class DataRepository : IDataRepository
{
    private String filePath;
    private DataRoot dataRoot;

    public DataRepository(String filePath)
    {
        this.filePath = filePath;
        this.dataRoot = JObject.Parse(ReadData()).ToObject<DataRoot>()!;
    }

    public DataRoot GetData()
    {
        return this.dataRoot;
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
        var filterdContets = this.dataRoot?.data?.contents?.Where(content => content.contentId != id).ToList();
        if (filterdContets != null && this.dataRoot?.data != null ){
            var updatedData =  this.dataRoot.data with { contents = filterdContets};
            this.dataRoot = this.dataRoot with {data = updatedData };
        }
    }

    public DataRoot SearchByTitle(String title)
    {
            var updatedData = this.dataRoot!.data! with { contents = this.dataRoot!.data!.contents!.Where(content => content?.title?.Contains(title) ?? false).ToList() } ;
            return this.dataRoot with {data = updatedData};
    }
}

