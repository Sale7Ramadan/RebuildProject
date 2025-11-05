using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Services
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
    public class CreateCategoryDto
    {
        //[Required(ErrorMessage = "اسم التصنيف مطلوب.")]
        //[StringLength(100, ErrorMessage = "طول اسم التصنيف يجب ألا يتجاوز 100 حرف.")]
        public string CategoryName { get; set; }
    }
    public class UpdateCategoryDto
    {
    //    [Required(ErrorMessage = "اسم التصنيف مطلوب.")]
    //    [StringLength(100, ErrorMessage = "طول اسم التصنيف يجب ألا يتجاوز 100 حرف.")]
        public string CategoryName { get; set; }
    }






}
