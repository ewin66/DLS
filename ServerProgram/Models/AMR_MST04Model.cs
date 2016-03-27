using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ServerProgram
{
    public class AMR_MST04Model : IBaseModel
    {

        UInt32 mst04sno;
        string mst04cmp;
        string mst04don;
        UInt32 mst04flr;
        string mst04hno;
        string mst04nam;
        string mst04phn;

        public UInt32 MST04SNO { get { return mst04sno; } set { mst04sno = value; } }
        public string MST04CMP { get { return mst04cmp; } set { mst04cmp = value; } }
        public string MST04DON { get { return mst04don; } set { mst04don = value; } }
        public UInt32 MST04FLR { get { return mst04flr; } set { mst04flr = value; } }
        public string MST04HNO { get { return mst04hno; } set { mst04hno = value; } }
        public string MST04NAM { get { return mst04nam; } set { mst04nam = value; } }
        public string MST04PHN { get { return mst04phn; } set { mst04phn = value; } }
        public AMR_MST04Model()
        {
        }

        public AMR_MST04Model(AMR_MST04Model info)
        {
            this.Assign(info);
        }

        public void Assign(AMR_MST04Model info)
        {
            this.MST04SNO = info.MST04SNO;
            this.MST04CMP = info.MST04CMP;
            this.MST04DON = info.MST04DON;
            this.MST04FLR = info.MST04FLR;
            this.MST04HNO = info.MST04HNO;
            this.MST04NAM = info.MST04NAM;
            this.MST04PHN = info.MST04PHN;

        }
        public AMR_MST04Model Clone()
        {
            return new AMR_MST04Model(this);
        }

        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public string Sensor { get; set; }
        public DataTable DataTable { get; set; }

        public int CurrentRow { get; set; }

        #region IBaseModel

        public bool Save()
        {
            return true;
        }

        #endregion IBaseModel
    }
}
