namespace CuteDev.Users.Data.DAL.Model
{
    using Database.DAL;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Permissions : BaseModel
    {
        [Required]
        [StringLength(50)]
        public string code { get; set; }

        [Required]
        [StringLength(100)]
        public string title { get; set; }

        [StringLength(1000)]
        public string description { get; set; }
    }
}
