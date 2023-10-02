using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verification.DomainModel
{
    public class StateModel
    {
        [Key]
        public int Id { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; } = true;

        public DateTime DateIs { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Name Field can not be Blank")]
        public string Name { get; set; }

        [Required(ErrorMessage = "State Zone Name Field is requird")]
        public string StateZone { get; set; }        
    }
}
