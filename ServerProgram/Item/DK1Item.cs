using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraCharts;

namespace DevExpress.ProductsDemo.Win.Item
{
    [Serializable]
    class DK1Item
    {
        public byte STX { get; set; }
        public byte LEN { get; set; }
        public UInt32 ID { get; set; }
        public byte CMD { get; set; }
        public byte ADD { get; set; }
        public byte XOR { get; set; }

    }

    [Serializable]
    class DK1Sensor
    {
        public UInt32[] SENSOR = new UInt32[6];
        public byte[] ERROR = new byte[6];

    }
    [Serializable]
    class DK1Data : DK1Sensor
    {
        public UInt32 DONG { get; set; }
        public UInt32 HO { get; set; }

    }

    public class ChartVariable2
    {
        public SeriesPoint data { get; set; }

        public ChartVariable2()
        {
            data = new SeriesPoint();
        }
    }

}
