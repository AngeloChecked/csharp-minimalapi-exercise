namespace MinimalApi;

interface IDataRepository
{
    public String GetData();
}

class DataRepository : IDataRepository
{
    private String filePath;

    public DataRepository(string filePath)
    {
        this.filePath = filePath;
    }

    public String GetData()
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
}
