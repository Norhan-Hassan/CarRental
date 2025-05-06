using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entities.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey(nameof(customer))]
        public string customerId { get; set; }
        public ApplicationUser customer { get; set; }



        [ForeignKey(nameof(car))]
        public int carId { get; set; }
        public Car car { get; set; }
    }
}
