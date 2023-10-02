using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verification.EntityModel
{
    [Table("tbl_Area")]
    public class Area
    {
        [Key]
        [Column("Id", TypeName = "Int", Order = 1)]
        public int Id { get; set; }

        [Column("IsActive", TypeName = "bit", Order = 2)]
        public bool IsActive { get; set; } = true;
        
        [Column("DateIs", TypeName = "Datetime", Order = 3)]
        public DateTime DateIs { get; set; } = DateTime.Now;

        [Column("Name", TypeName = "Varchar(500)", Order = 4)]
        public string Name { get; set; }

        [Column("Pincode", TypeName = "Varchar(6)", Order = 5)]
        public string Pincode { get; set; } = "0".PadLeft(6, '0');

        [Column("PinSno", TypeName = "Int", Order = 6)]
        public int? PinSno { get; set; }

        [Column("VirPin", TypeName = "Varchar(8)", Order = 7)]
        public string VirPin { get; set; } = "0".PadLeft(8, '0');

        [Column("AreaZoneId", TypeName = "Int", Order = 9)]
        public int AreaZoneId { get; set; }

        public AreaZone AreaZone { get; set; }
    }
}
