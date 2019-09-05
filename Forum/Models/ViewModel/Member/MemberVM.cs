using Forum.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Models.ViewModel.Member
{
    public class MemberVM
    {
        public MemberVM()
        {

        }
        public MemberVM(MemberDTO row)
        {
            Account = row.Account;
            Password = row.Password;
            Name = row.Name;
            Email = row.Email;
            IsAdmin = row.IsAdmin;
        }

        
        public string Account { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}