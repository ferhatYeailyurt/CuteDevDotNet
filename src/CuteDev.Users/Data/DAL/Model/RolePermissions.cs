namespace CuteDev.Users.Data.DAL.Model
{
    using Database.DAL;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RolesPermissions : BaseModel
    {
        [Required]
        public int rol_Id { get; set; }

        [Required]
        public int permission_Id { get; set; }
    }
}
