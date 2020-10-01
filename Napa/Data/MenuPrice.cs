using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Napa.Data
{
    public class MenuPrice
    {
        public Int32 Id { get; set; }
        public MenuItem MenuItem { get; set; }
        public Int32 MenuItemId { get; set; }

        public MenuCategoryPriceKind MenuPriceKind { get; set; }
        public Int32 MenuPriceKindId { get; set; }
        public Single Value { get; set; }
    }
}
