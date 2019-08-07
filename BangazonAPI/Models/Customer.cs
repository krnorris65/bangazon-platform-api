using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BangazonAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        List<PaymentType> PaymentTypes { get; set; } = new List<PaymentType>();
        List<Product> Products { get; set; } = new List<Product>();
    }
}