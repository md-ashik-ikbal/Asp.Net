using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD.DTOS
{
    public class CreateStudentDTO
    {
        //public int Id { get; set; }
        [Required(ErrorMessage = "Name could not be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Cgpa is required")]
        public string Cgpa { get; set; }
    }
}