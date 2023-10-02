using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Verification.DomainModel
{
    public class StateZoneModel
    {
        [Key]
        public int Id { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; } = true;

        public DateTime DateIs { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Name Field can not be Blank")]
        public string Name { get; set; }
    }
}
