using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanovych_2
{
    class TreeNode<T>
    {
        T data;
        TreeNode<T> leftNode;
        TreeNode<T> rightNode;

        public TreeNode(T inData)
        {
            data = inData;
        }

        public T GetValue
        {
            get { return data; }
        }
    }
}
