using Microsoft.AspNetCore.Mvc;

namespace SOC.Conductor.Entities
{
    public class Workflow
    {
        public int Id { get; set; } 

        public string Name { get; set; }    

        public int Version { get; set; }    

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool Enabled { get; set; }   

        public ICollection<Trigger> Triggers { get; set;}

    }
}
