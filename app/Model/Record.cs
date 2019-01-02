namespace Model
{
    public class Record
    {
        public string DatasetId { get; set; }
        public string RecordId { get; set; }
        public Field Fields { get; set; }
        public string Record_timestamp { get; set; }
    }
}