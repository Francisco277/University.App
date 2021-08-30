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

        [Required(ErrorMessage = "The LastName is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The FirstMidName is Required")]
        public string FirstMidName { get; set; }
        [Required(ErrorMessage = "The EnrollmentDate is Required")]
        public DateTime EnrollmentDate { get; set; }
    }
}
