using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevExpress.ProductsDemo.Win.DB
{
    public abstract class Worker
    {
        public abstract void DoWork();
        public abstract void RequestStop();
    }
}
