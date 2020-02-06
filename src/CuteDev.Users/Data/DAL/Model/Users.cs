namespace CuteDev.Users.Data.DAL.Model
{
    using Database.DAL;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users : BaseModel
    {
        [Required]
        [StringLength(250)]
        public string email { get; set; }

        [Required]
        [StringLength(250)]
        public string password { get; set; }

        [Required]
        [StringLength(200)]
        public string fullname { get; set; }

        [StringLength(50)]
        public string role { get; set; }
    }
}
