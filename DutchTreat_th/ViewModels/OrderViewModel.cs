using DutchTreat_th.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat_th.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        [Required]
        [MinLength(4)]
        public String OrderNumber { get; set; }


        public ICollection<OrderItemViewModel> Items { get; set; }
    }
}
