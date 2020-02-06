namespace CuteDev.Users.Data.DAL.Model
{
    using Database.DAL;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UsersMeta")]
    public partial class UsersMeta: BaseModel
    {
        public int user_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string metaKey { get; set; }

        [StringLength(500)]
        public string metaValue { get; set; }
    }
}
