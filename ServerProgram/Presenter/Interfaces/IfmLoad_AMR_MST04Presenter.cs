using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ServerProgram
{
    public interface IfmLoad_AMR_MST04Presenter
    {
        /// <summary>
        /// 현재 컨트롤러와 연결된 폼(Form)을 지정/반환합니다.
        /// </summary>
        IfmLoad_AMR_MST04 CurrentForm { get; set; }

        /// <summary>
        /// 초기로드 내용을 정의합니다.
        /// </summary>
        //void Load();
    }
}
