namespace Domain.Entities
{
    public class WarehouseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string Photo { get; set; }
        public int? DisplaySequence { get; set; }
    }
}
