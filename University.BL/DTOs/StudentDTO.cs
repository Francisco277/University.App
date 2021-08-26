using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
   public class StudentDTO
    {
        [Required(ErrorMessage ="The StudentID is Required")]
        public int ID { get; set; }

        [Required(ErrorMessage ="The StudentFullName is Required")]
        public string FullName { get; set; }
    }
}
