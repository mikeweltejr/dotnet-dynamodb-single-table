using System;

namespace Movies.API.Models
{
    public class User
    {
        public string PK { get; set; }
        public string SK { get; set; } 
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public DateTime DOB { get; set; }

        public User()
        {
            PK = SortKeyPrefixes.USER;
            SK = SortKeyPrefixes.USER + Id;
        }
    }
}