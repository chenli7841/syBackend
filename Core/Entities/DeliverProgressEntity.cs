namespace Domain.Entities
{
    public class DeliverProgressEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Percent { get; set; }
        public bool IsDeleted { get; set; }
        //public int Sequence { get; set; }
        //public bool? IsMain { get; set; }
        public string Description { get; set; }
        public int RouteId { get; set; }

        public RouteEntity Route { get; set; }
    }
}
