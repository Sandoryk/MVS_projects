using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic_5_Task_1
{
    class OverStackEventArgs:EventArgs
    {
        long filesize;
        public  OverStackEventArgs(long size)
        {
            filesize = size;
        }
        public long FileSize { get { return filesize; } }
    }
}
