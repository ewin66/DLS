using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerProgram
{
    public interface IG02I02ModulePresenter
    {
        /// <summary>
        /// 현재 컨트롤러와 연결된 폼(Form)을 지정/반환합니다.
        /// </summary>
        IG02I02Module CurrentForm { get; set; }
    }
}
