using System;

namespace TestMVC.Model.DTO.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public bool Status { get; set; }
        public char Genre { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
