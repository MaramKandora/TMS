using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain.Entities.People;
using TMS.Domain.Entities.Users;

namespace TMS.Application.DTOs.Users
{
    public class    UserToAddDTO
    {

        // Add User Info

        public string UserName { get; set; } = string.Empty; 
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;


        // Person Info
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }




    }
}
