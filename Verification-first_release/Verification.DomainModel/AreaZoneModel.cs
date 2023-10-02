using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Verification.DomainModel
{
    public class AreaZoneModel
    {
        [Key]
        public int Id { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime DateIs { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Name Field can not be Blank")]
        public string Name { get; set; }

        [Required(ErrorMessage = "State Name Field is requird")]
        public string State { get; set; }
    }
}
