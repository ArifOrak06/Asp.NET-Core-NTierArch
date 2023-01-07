using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierApp.Core.DTOs
{
    public class ProductWithCategoriesDto : ProductDto
    {
        public CategoryDto Category { get; set; }
    }
}
