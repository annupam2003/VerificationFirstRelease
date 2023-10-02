using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verification.DomainModel
{
   public class AreaModel
    {
        [Key]
        public int Id { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime DateIs { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Name Field can not be Blank")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pincode Field can not be Blank")]
        public string Pincode { get; set; } = "0".PadLeft(6,'0');

        public int PinSno { get; set; }

        public string VirPin { get; set; } = "0".PadLeft(8, '0');

        public string AreaZone { get; set; }    //AreaZone Name
    }
}
