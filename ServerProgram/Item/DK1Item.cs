using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevExpress.ProductsDemo.Win.Item
{
    [Serializable]
    class DK1Item
    {
        public byte STX { get; set; }
        public byte LEN { get; set; }
        public UInt32 ID { get; set; }
        public byte CMD { get; set; }
        //public byte[] DATA { get; set; }
        public byte ADD { get; set; }
        public byte XOR { get; set; }

        //public DK1Item(int size)
        //{
        //    DATA = new byte[size];
        //}
    }
}
