using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerProgram
{
    public class G02I01ModulePresenter : IG02I01ModulePresenter
    {
        public IG02I01Module CurrentForm { get; set; }

        /// <summary>
        /// 생성자
        /// </summary>
        public G02I01ModulePresenter(IG02I01Module view)
        {
            CurrentForm = view;
            CurrentForm.Tab1SearchEvent += new EventHandler(CurrentForm_OnTab1Search);
            
        }

        void CurrentForm_OnTab1Search(object sender, EventArgs e)
        {
            AMR_MST04Model model = new AMR_MST04Model();
            CurrentForm.Tab1SearchComplete(model);

        }
    }
}
