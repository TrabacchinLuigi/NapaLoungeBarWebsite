using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Napa.Data
{
    public class MenuCategoryPriceKind : IOrderable, INamed
    {
        public Int32 Id { get; set; }
        public Int32 Order { get; set; }
        public String Name { get; set; }
        public List<MenuPrice> Prices { get; set; }
        public MenuCategory Category { get; set; }
        public Int32 CategoryId { get; set; }
    }
}
