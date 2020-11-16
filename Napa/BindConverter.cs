using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Napa
{
    class BindConverter<TFrom, TTo>
    {
        private readonly Func<TFrom> _getter;
        private readonly Action<TFrom> _setter;
        private readonly Func<TFrom, TTo> _convert;
        private readonly Func<TTo, TFrom> _convertBack;

        public TTo ConvertedProperty
        {
            get => _convert(_getter());
            set
            {
                _setter(_convertBack(value));
            }
        }
        public BindConverter(Func<TFrom> getter, Action<TFrom> setter, Func<TFrom, TTo> convert, Func<TTo, TFrom> convertBack)
        {
            _getter = getter;
            _setter = setter;
            _convert = convert;
            _convertBack = convertBack;
        }
    }
}
