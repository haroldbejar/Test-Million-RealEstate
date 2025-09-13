using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyService.Domain.Models
{
    public class PropertySearchParams
    {
        public int? Bedrooms { get; set; }
        public string Title { get; set; }
        public Nullable<decimal> MinAmount { get; set; }
        public Nullable<decimal> MaxAmount { get; set; }


    }
}