using System;

namespace Movies.API.Dtos
{
    public class UserCreateDto
    {
        public string Name { get; set; }
        public DateTime DOB { get; set; }
    }
}