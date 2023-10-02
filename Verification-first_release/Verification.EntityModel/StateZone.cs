using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Verification.EntityModel
{
    [Table("tbl_StateZone")]
    public class StateZone
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

        public ICollection<State> States { get; set; }
    }
}
