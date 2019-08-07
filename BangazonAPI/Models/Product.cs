using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonAPI.Models
{
    public class Product
    {
        //       Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
        //ProductTypeId INTEGER NOT NULL,
        //   CustomerId INTEGER NOT NULL,
        //Price MONEY NOT NULL,
        //   Title VARCHAR(255) NOT NULL,

        //   [Description] VARCHAR(255) NOT NULL,
        //   Quantity INTEGER NOT NULL,
        //   CONSTRAINT FK_Product_ProductType FOREIGN KEY(ProductTypeId) REFERENCES ProductType(Id),
        //   CONSTRAINT FK_Product_Customer FOREIGN KEY(CustomerId) REFERENCES Customer(Id)

        public int Id { get; set; }
        [Required]
        public int ProductTypeId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }

        public ProductType ProductType { get; set; }

        public Customer Customer { get; set; }
    }
}
