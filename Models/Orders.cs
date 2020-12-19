using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public partial class Orders
    {
        [Key]
        public int Id { get; set; }
        public int Userid { get; set; }
        public double? Totalprice { get; set; }
        public double? Discount { get; set; }
        public string Paymenttype { get; set; }
    }
}
