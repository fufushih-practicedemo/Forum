using Forum.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Models.ViewModel.Member
{
    public class MemberProfileVM
    {
        public MemberProfileVM()
        {

        }

        public MemberProfileVM(MemberDTO row)
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
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public string ConfirmPassword { get; set; }
    }
}