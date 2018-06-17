namespace DotNetCore.Data.Entities
{
    public class EventType : Entity
    {
        public string Name { get; set; }

        public string GedcomTag { get; set; }

        public string Sentence { get; set; }

        public bool IsFamilyEvent { get; set; }

        public bool UseDate { get; set; }

        public bool UsePlace { get; set; }

        public bool UseDescription { get; set; }
    }
}
