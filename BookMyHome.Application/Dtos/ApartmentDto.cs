using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyHome.Application.Dtos
{
    public class ApartmentDto
    {
        public Guid ApartmentID { get; set; }
        public Guid UserID { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool AvailabiltyStatus { get; set; }
    }
}
