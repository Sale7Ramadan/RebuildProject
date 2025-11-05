using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Services
{
    public class CityDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
    }

    public class CreateCityDto
    {
        //[Required(ErrorMessage = "اسم المدينة مطلوب.")]
        //[StringLength(100, ErrorMessage = "طول اسم المدينة يجب ألا يتجاوز 100 حرف.")]
        public string CityName { get; set; }
    }


    public class UpdateCityDto
    {
        //[Required(ErrorMessage = "اسم المدينة مطلوب.")]
        //[StringLength(100, ErrorMessage = "طول اسم المدينة يجب ألا يتجاوز 100 حرف.")]
        public string CityName { get; set; }
    }



}
