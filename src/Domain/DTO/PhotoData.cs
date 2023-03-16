namespace Domain.DTO;

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