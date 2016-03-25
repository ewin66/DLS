using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerProgram
{
    public interface IBaseSubView
    {
        void DataBinding(IBaseModel datalist);
    }
}
