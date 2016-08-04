using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanovych_2
{
    class TreeNode<T> : IEnumerable<T>
    {
        T data;
        TreeNode<T> parent;
        TreeNode<T> leftNode;
        TreeNode<T> rightNode;

        public TreeNode(T inData)
        {
            data = inData;
        }
        public TreeNode<T> Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public TreeNode<T> LeftNode
        {
            get { return leftNode; }
            set { leftNode = value; }
        }
        public TreeNode<T> RightNode
        {
            get { return rightNode; }
            set { rightNode = value; }
        }

        public T GetValue
        {
            get { return data; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (LeftNode != null)
            {
                foreach (var child in LeftNode)
                {
                    yield return child;
                }
            }

            yield return GetValue;

            if (RightNode != null)
            {
                foreach (var child in RightNode)
                {
                    yield return child;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
