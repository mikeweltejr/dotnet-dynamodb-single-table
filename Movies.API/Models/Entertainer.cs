using System;

namespace Movies.API.Models
{
    public class Entertainer
    {
        public string PK { get; set; }
        public string SK { get; set; }
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        public Entertainer()
        {
            PK = SortKeyPrefixes.ENTERTAINER;
            SK = SortKeyPrefixes.ENTERTAINER + Id;
        }
    }
}