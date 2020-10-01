using Napa.Data.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Napa.Data
{
    public class MenuCategory : IOrderable, INamed
    {
        public Int32 Id { get; set; }
        public Int32 Order { get; set; }
        public String Name { get; set; }
        public Boolean Show { get; set; }
        public String ImageUrl { get; set; }
        public List<MenuCategoryPriceKind> PriceKinds { get; } = new List<MenuCategoryPriceKind>();
        public List<MenuItem> Items { get; } = new List<MenuItem>();
    }
}
