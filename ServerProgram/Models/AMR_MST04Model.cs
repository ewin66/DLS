using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ServerProgram
{
    public class AMR_MST04Model : IBaseModel
    {

        /// <summary>
        /// 생성자
        /// </summary>
        public AMR_MST04Model()
        {
        }


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
