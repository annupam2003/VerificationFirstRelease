using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verification.EntityModel
{
    [Table("tbl_State")]
    public class State
    {
        [Key]
        [Column("Id", TypeName = "Int", Order = 1)]
        public int Id { get; set; }

        [Column("IsActive", TypeName = "bit", Order = 2)]
        public bool? IsActive { get; set; }

        [Column("DateIs", TypeName = "Datetime", Order = 3)]
        public DateTime DateIs { get; set; }

        [Column("Name", TypeName = "Varchar(500)", Order = 4)]
        public string Name { get; set; }

        [Column("StateZoneId", TypeName = "Int", Order = 5)]
        public int StateZoneId { get; set; }

        public StateZone StateZone { get; set; }

        public ICollection<AreaZone> AreaZones { get; set; }
    }
}

