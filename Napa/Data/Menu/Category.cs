using Napa.Data.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Napa.Data.Menu
{
    public class Category : IOrderable, INamed
    {
        public Int32 Id { get; set; }
        public Int32 Order { get; set; }
        public String Name { get; set; }
        public Boolean Show { get; set; }
        public String ImageUrl { get; set; }
        public List<CategoryPriceKind> PriceKinds { get; } = new List<CategoryPriceKind>();
        public List<Item> Items { get; } = new List<Item>();
    }
}
