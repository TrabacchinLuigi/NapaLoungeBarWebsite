using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Napa.Data.Menu
{
    public class Item : IOrderable, INamed
    {
        public Int32 Id { get; set; }
        public Int32 Order { get; set; }
        public Category Category { get; set; }
        public Int32 CategoryId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Boolean Show { get; set; }
        public List<Price> Prices { get; set; }
    }
}
