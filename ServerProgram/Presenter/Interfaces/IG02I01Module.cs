using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerProgram
{
    public interface IG02I01Module
    {
        /// <summary>
        /// 현재 사용 데이터모델을 지정/반환합니다.
        /// </summary>
        IBaseModel CurrentData { get; set; }

        /// <summary>
        /// 컨트롤러를 지정/반환합니다.
        /// </summary>
        IG02I01ModulePresenter MainPresenter { get; set; }


        void ShowMessage(string msg);


        event EventHandler Tab1SearchEvent;
        void Tab1SearchComplete(IBaseModel item);
    }
}
