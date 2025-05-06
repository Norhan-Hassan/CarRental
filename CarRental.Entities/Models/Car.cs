using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace CarRental.Entities.Models
{
    public class Car
    {
        [Key]
        public int ID { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        [ValidateNever]
        public ICollection<Rental> Rentals { get; set; }
    }
}
