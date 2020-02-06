namespace CuteDev.Log.Data.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Logs
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal id { get; set; }

        [Required]
        [StringLength(50)]
        public string guid { get; set; }

        public DateTime createDate { get; set; }

        public decimal creatorId { get; set; }

        [StringLength(50)]
        public string creatorIP { get; set; }

        [StringLength(50)]
        public string logLevel { get; set; }

        [Required]
        public string message { get; set; }

        [StringLength(50)]
        public string appName { get; set; }

        public string logData { get; set; }
    }
}
