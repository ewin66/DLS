using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerProgram
{
    public static class CommandCenter
    {
        /// <summary>
        /// G02I01Module에서 명령 전달
        /// </summary>
        public static StaticCommand StateChanged { get; private set; }

        /// <summary>
        /// G02I02Module에서 명령 전달
        /// </summary>
        public static StaticCommand GraphSearchChanged { get; private set; }

        /// <summary>
        /// G03I01Module에서 명령 전달
        /// </summary>
        public static StaticCommand ReadingChanged { get; private set; }


        static CommandCenter()
        {
            StateChanged = new StaticCommand("StateChanged");
            GraphSearchChanged = new StaticCommand("GraphSearchChanged");
            ReadingChanged = new StaticCommand("ReadingChanged");
        }
    }
}
