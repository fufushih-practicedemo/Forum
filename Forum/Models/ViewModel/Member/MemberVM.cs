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
            UID = row.UID;
            Account = row.Account;
            Password = row.Password;
            Name = row.Name;
            Email = row.Email;
        }

        public int UID { get; set; }
        [Required]
        public string Account { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}