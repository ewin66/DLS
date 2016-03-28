using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerProgram
{
    public interface IfmMain
    {
        /// <summary>
        /// 현재 사용 데이터모델을 지정/반환합니다.
        /// </summary>
        IBaseModel CurrentData { get; set; }

        /// <summary>
        /// 컨트롤러를 지정/반환합니다.
        /// </summary>
        IfmMainPresenter MainPresenter { get; set; }

        void ShowMessage(string msg);

    }
}
