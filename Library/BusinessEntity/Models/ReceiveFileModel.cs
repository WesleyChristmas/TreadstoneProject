namespace BusinessEntity.Models
{
    public class ReceiveFileModel
    {
        public byte[] Data { get; set; }
        public string FileName { get; set; }
        public int IdSection { get; set; }
        public string ServerPath { get; set; }
    }
}
