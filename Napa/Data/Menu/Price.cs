using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Napa.Data.Menu
{
    public class Price
    {
        public Int32 Id { get; set; }
        public Item MenuItem { get; set; }
        public Int32 MenuItemId { get; set; }

        public CategoryPriceKind MenuPriceKind { get; set; }
        public Int32 MenuPriceKindId { get; set; }
        public Single Value { get; set; }
    }
}
