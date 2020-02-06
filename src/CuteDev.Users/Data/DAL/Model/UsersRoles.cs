namespace CuteDev.Users.Data.DAL.Model
{
    using Database.DAL;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UsersRoles : BaseModel
    {
        [Required]
        public int user_Id { get; set; }

        [Required]
        public int role_Id { get; set; }
    }
}
