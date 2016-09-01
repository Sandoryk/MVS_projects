using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanovych_2
{
    public enum TreeAction
    {
        IsDeleted,
        IsAdded
    }
    public class TreeItemActionEventArgs<T> : EventArgs
    {
        public readonly TreeAction action;

        public TreeItemActionEventArgs(TreeAction curAct)
        {
            action = curAct;
        }
    }
}
