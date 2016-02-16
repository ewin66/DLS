using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace DevExpress.ProductsDemo.Win.Common
{
    public static class Extension
    {
        /// <summary>
        /// The invoke on main thread.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        /// <param name="act">
        /// The act.
        /// </param>
        public static void InvokeOnMainThread(this System.Windows.Forms.Control control, Action act)
        {
            if (control.IsHandleCreated)
            {
                control.Invoke(new MethodInvoker(act), null);
            }
        }


        public static TResult SafeInvoke<T, TResult>(this T isi, Func<T, TResult> call) where T : ISynchronizeInvoke
        {
            if (isi.InvokeRequired)
            {
                IAsyncResult result = isi.BeginInvoke(call, new object[] { isi });
                object endResult = isi.EndInvoke(result); return (TResult)endResult;
            }
            else
                return call(isi);
        }
        public static void SafeInvoke<T>(this T isi, Action<T> call) where T : ISynchronizeInvoke
        {
            if (isi.InvokeRequired) isi.BeginInvoke(call, new object[] { isi });
            else
                call(isi);
        }
    }
}
