namespace BusinessLogic.Entities;

public class PhotoData
{
    public byte[] Data { get; set; }

    public PhotoData(byte[] data)
    {
        this.Data = data;
    }
    public PhotoData()
    {
    }
}