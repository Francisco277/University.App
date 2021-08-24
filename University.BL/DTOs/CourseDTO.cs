using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public class CourseDTO
    {
        [Required(ErrorMessage ="The ID es Required")]
        public int CourseID { get; set; }

        [Required(ErrorMessage = "The Title es Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Credits es Required")]
        
        public int Credits { get; set; }

     }
}
