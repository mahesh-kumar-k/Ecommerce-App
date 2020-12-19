using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public partial class Carts
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProductData { get; set; }
        public bool IsOrderPlaced { get; set; }
        public int? OrderId { get; set; }
    }
}
