using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuteDev.Database.DAL
{
    public class BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int id { get; set; }

        [Required]
        [Column(Order = 1)]
        [StringLength(50)]
        public string guid { get; set; }

        [Required]
        [Column(Order = 2)]
        [StringLength(50)]
        public string createIp { get; set; }

        [Column(Order = 3)]
        public DateTime createDate { get; set; }

        [Column(Order = 4)]
        public int creatorId { get; set; }

        [StringLength(50)]
        [Column(Order = 5)]
        public string updateIp { get; set; }

        [Column(Order = 6)]
        public DateTime? updateDate { get; set; }

        [Column(Order = 7)]
        public int? updaterId { get; set; }

        [Column(Order = 8)]
        public bool deleted { get; set; }

        public BaseModel()
        {
            this.guid = Guid.NewGuid().ToString();
        }
    }
}
