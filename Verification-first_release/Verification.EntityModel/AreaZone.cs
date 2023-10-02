using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Verification.EntityModel
{
    [Table("tbl_AreaZone")]
    public class AreaZone
    {
        [Key]
        [Column("Id", TypeName = "Int", Order = 1)]
        public int Id { get; set; }

        [Column("IsActive", TypeName = "bit", Order = 2)]
        public bool IsActive { get; set; }

        [Column("DateIs", TypeName = "Datetime", Order = 3)]
        public DateTime DateIs { get; set; }

        [Column("Name", TypeName = "Varchar(500)", Order = 4)]
        public string Name { get; set; }

        [Column("StateId", TypeName = "Int", Order = 5)]
        public int StateId { get; set; }

        public State State { get; set; }

        public ICollection<Area> Areas { get; set; }
    }
}
