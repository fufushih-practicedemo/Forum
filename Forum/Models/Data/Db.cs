using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Forum.Models.Data
{
    public class Db : DbContext
    {
        public DbSet<MemberDTO> Members { get; set; }
        public DbSet<ArticleDTO> Articles { get; set; }
        public DbSet<MessageDTO> Messages { get; set; }
        public DbSet<AlbumDTO> Albums { get; set; }

        public DbSet<RoleDTO> Roles { get; set; }
        public DbSet<UserRoleDTO> UserRoles { get; set; }
    }
}