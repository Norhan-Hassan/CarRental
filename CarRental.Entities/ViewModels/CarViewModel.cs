using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entities.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Brand Name is Required")]
        [MaxLength(50)]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        public double Price { get; set; }

        [Range(0, int.MaxValue,ErrorMessage ="You have exceeded the possible range")]
        [Required(ErrorMessage = "Quantity is Required")]
        public int Quantity { get; set; }
    }
}
