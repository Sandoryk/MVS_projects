using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanovych_2
{
    class BiTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private int count;
        TreeNode<T> root;
        IComparer<T> defComp;

        public event EventHandler<TreeItemActionEventArgs<T>> ItemAdded;
        public event EventHandler<TreeItemActionEventArgs<T>> ItemRemoved;

        public BiTree()
        {
            defComp = Comparer<T>.Default;
        }
        public BiTree(IComparer<T> comparator)
        {
            defComp = comparator;
        }

        public int GetTreeCount
        {
            get { return count; }
        }

        public T GetValue
        {
            get { return root.GetValue; }
        }


        public void Insert(T inData)
        {
            if (root==null)
            {
                root = new TreeNode<T>(inData);
                count++;
                if (ItemAdded != null)
                    ItemAdded(this, new TreeItemActionEventArgs<T>(TreeAction.IsAdded));
            }
            else
            {
                TreeNode<T> parent = root;
                bool insertedf = false;
                TreeNode<T> newNode = null;
                while (!insertedf)
                {
                    int c = defComp.Compare(parent.GetValue, inData);
                    if (c >= 0) //unique ==1
                    { // insert in left subtree
                        if (parent.LeftNode == null)
                        { 
                            newNode = new TreeNode<T>(inData);
                            parent.LeftNode = newNode;
                            insertedf = true;
                        }
                        else
                        { 
                            parent = parent.LeftNode;
                        }
                    }
                    else if(c == -1)
                    { 
                        if (parent.RightNode == null)
                        { 
                            newNode = new TreeNode<T>(inData);
                            parent.RightNode = newNode;
                            insertedf = true;
                        }
                        else
                        {
                            parent = parent.RightNode;
                        }
                    }
                    //else
                        //throw new Exception("Node already exists");
                }
                newNode.Parent = parent;
                count++;
            }

            /*if (this.value == null)
            {
                this.value = inData;
                if (ItemAdded != null)
                    ItemAdded(this, new TreeItemActionEventArgs<T>(TreeAction.IsAdded));
            }
                
            else
            {
                if (defComp.Compare(this.value, inData) >= 0) //unique ==1
                {
                    if (leftNode == null)
                    {
                        this.leftNode = new BiTree<T>(defComp);
                        this.leftNode.parent = this;
                    }

                    leftNode.Insert(inData);
                }
                else if (defComp.Compare(this.value, inData) == -1)
                {
                    if (rightNode == null)
                    {
                        this.rightNode = new BiTree<T>(defComp);
                        this.rightNode.parent = this;
                    }
                        
                    rightNode.Insert(inData);
                }
                //else
                    //throw new Exception("Node already exists");
            }

            this.count = Recount(this);*/
        }
 
        protected TreeNode<T> recursiveSearch(TreeNode<T> root, T key)
        {
            if (root == null)
            {
                return null;
            }
            int c = defComp.Compare(root.GetValue, key);
            if (c == 0)
            {
                return root;
            }
            if (c < 0)
            {
                return recursiveSearch(root.LeftNode, key);
            }
            else {
                return recursiveSearch(root.RightNode, key);
            }
        }

        public TreeNode<T> Search(T key)
        {
            if (root==null)
            {
                return null;
            }
            return recursiveSearch(root, key);
        }

        /*public int Recount(BiTree<T> t)
        {
            int count = 0;

            if (t.leftNode != null)
                count += Recount(t.leftNode);

            count++;

            if (t.rightNode != null)
                count += Recount(t.rightNode);

            return count;
        }*/

        public bool IsEmpty()
        {
            if (root == null)
                return true;
            else
                return false;
        }

        public bool Remove(T objToRemove)
        {
            TreeNode<T> currentNode = null, childParent = null;
            int result = 0;

            if (this.IsEmpty()) { // can't delete from an empty tree
    		    //throw new NoSuchElementException();
                return false;
    	    }
            TreeNode<T> node = Search(objToRemove);
            if (node == null)
            {
                throw new Exception("No such node for deletion");
            }
            currentNode = node;
            childParent = node.Parent;

            // We now need to "rethread" the tree
            // CASE 1: If current has no right child, then current's left child becomes
            //         the node pointed to by the parent
            if (currentNode.RightNode == null)
            {
                if (childParent == null)
                    currentNode = currentNode.LeftNode;
                else
                {
                    result = defComp.Compare(childParent.GetValue, currentNode.GetValue);
                    if (result >= 0)
                        // parent.Value > current.Value, so make current's left child a left child of parent
                        childParent.LeftNode = currentNode.LeftNode;
                    else if (result < 0)
                        // parent.Value < current.Value, so make current's left child a right child of parent
                        childParent.RightNode = currentNode.LeftNode;
                }
            }
            // CASE 2: If current's right child has no left child, then current's right child
            //         replaces current in the tree
            else if (currentNode.RightNode.LeftNode == null)
            {
                currentNode.RightNode.LeftNode = currentNode.LeftNode;

                if (childParent == null)
                    currentNode = currentNode.RightNode;
                else
                {
                    result = defComp.Compare(childParent.GetValue, currentNode.GetValue);
                    if (result >= 0)
                        // parent.Value > current.Value, so make current's right child a left child of parent
                        childParent.LeftNode = currentNode.RightNode;
                    else if (result < 0)
                        // parent.Value < current.Value, so make current's right child a right child of parent
                        childParent.RightNode = currentNode.RightNode;
                }
            }
            // CASE 3: If current's right child has a left child, replace current with current's
            //          right child's left-most descendent
            else
            {
                // We first need to find the right node's left-most child
                TreeNode<T> leftmost = currentNode.RightNode.LeftNode, 
                lmParent = currentNode.RightNode;

                while (leftmost.LeftNode != null)
                {
                    lmParent = leftmost;
                    leftmost = leftmost.LeftNode;
                }

                // the parent's left subtree becomes the leftmost's right subtree
                lmParent.LeftNode = leftmost.RightNode;

                // assign leftmost's left and right to current's left and right children
                leftmost.LeftNode = currentNode.LeftNode;
                leftmost.RightNode = currentNode.RightNode;

                if (childParent == null)
                    currentNode = leftmost;
                else
                {
                    result = defComp.Compare(childParent.GetValue, currentNode.GetValue);
                    if (result >= 0)
                        // parent.Value > current.Value, so make leftmost a left child of parent
                        childParent.LeftNode = leftmost;
                    else if (result < 0)
                        // parent.Value < current.Value, so make leftmost a right child of parent
                        childParent.RightNode = leftmost;
                }
            }

            count--;
            if (ItemRemoved != null)
                ItemRemoved(currentNode, new TreeItemActionEventArgs<T>(TreeAction.IsDeleted));
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        { 
            if (root.LeftNode != null)
            {
                foreach (var child in root.LeftNode)
                {
                    yield return child;
                }
            }

            yield return root.GetValue;

            if (root.RightNode != null)
            {
                foreach (var child in root.RightNode)
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
