using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Models.ViewModel.Member
{
    public class MemberLoginVM
    {
        [DisplayName("會員帳號")]
        [Required(ErrorMessage = "Please input account")]
        public string UserName { get; set; }

        [DisplayName("會員密碼")]
        [Required(ErrorMessage = "Please input password")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}