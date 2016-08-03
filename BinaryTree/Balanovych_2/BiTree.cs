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
        private T value;
        private int count;
        private BiTree<T> parent;
        private BiTree<T> leftNode;
        private BiTree<T> rightNode;
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
            get { return value; }
        }
        // вставка
        public void Insert(T inValue)
        {
            if (this.value == null)
            {
                this.value = inValue;
                if (ItemAdded != null)
                    ItemAdded(this, new TreeItemActionEventArgs<T>(TreeAction.IsAdded));
            }
                
            else
            {
                if (defComp.Compare(this.value,inValue) >= 0) //unique ==1
                {
                    if (leftNode == null)
                    {
                        this.leftNode = new BiTree<T>(defComp);
                        this.leftNode.parent = this;
                    }

                    leftNode.Insert(inValue);
                }
                else if (defComp.Compare(this.value, inValue) == -1)
                {
                    if (rightNode == null)
                    {
                        this.rightNode = new BiTree<T>(defComp);
                        this.rightNode.parent = this;
                    }
                        
                    rightNode.Insert(inValue);
                }
                //else
                    //throw new Exception("Узел уже существует");
            }

            this.count = Recount(this);
        }
        // поиск
        public BiTree<T> Search(T inValue)
        {
            if (this.value.Equals(inValue))
                return this;
            else if (defComp.Compare(this.value, inValue) == 1)
            {
                if (leftNode != null)
                    return this.leftNode.Search(inValue);
                else
                    return null;
                    //throw new Exception("Искомого узла в дереве нет");
            }
            else
            {
                if (rightNode != null)
                    return this.rightNode.Search(inValue);
                else
                    return null;  
                    //throw new Exception("Искомого узла в дереве нет");
            }
        }

        // подсчет
        public int Recount(BiTree<T> t)
        {
            int count = 0;

            if (t.leftNode != null)
                count += Recount(t.leftNode);

            count++;

            if (t.rightNode != null)
                count += Recount(t.rightNode);

            return count;
        }
        // очистка
        public void Clear()
        {
            this.value = default(T);
            this.leftNode = null;
            this.rightNode = null;
        }
        // проверка пустоты
        public bool IsEmpty()
        {
            if (this.value == null)
                return true;
            else
                return false;
        }

        //удаление
        public bool Remove(T value)
        {
            BiTree<T> current = null, childParent = null;
            int result = 0;

            if (this.IsEmpty()) { // can't delete from an empty tree
    		    //throw new NoSuchElementException();
                return false;
    	    }
            BiTree<T> node = Search(value);
            if (node == null)
            {
                throw new Exception("Нет такого элемента для удаления");
            }
            current = node;
            childParent = node.parent;

            // We now need to "rethread" the tree
            // CASE 1: If current has no right child, then current's left child becomes
            //         the node pointed to by the parent
            if (current.rightNode == null)
            {
                if (childParent == null)
                    current = current.leftNode;
                else
                {
                    result = defComp.Compare(childParent.value, current.value);
                    if (result >= 0)
                        // parent.Value > current.Value, so make current's left child a left child of parent
                        childParent.leftNode = current.leftNode;
                    else if (result < 0)
                        // parent.Value < current.Value, so make current's left child a right child of parent
                        childParent.rightNode = current.leftNode;
                }
            }
            // CASE 2: If current's right child has no left child, then current's right child
            //         replaces current in the tree
            else if (current.rightNode.leftNode == null)
            {
                current.rightNode.leftNode = current.leftNode;

                if (childParent == null)
                    current = current.rightNode;
                else
                {
                    result = defComp.Compare(childParent.value, current.value);
                    if (result >= 0)
                        // parent.Value > current.Value, so make current's right child a left child of parent
                        childParent.leftNode = current.rightNode;
                    else if (result < 0)
                        // parent.Value < current.Value, so make current's right child a right child of parent
                        childParent.rightNode = current.rightNode;
                }
            }
            // CASE 3: If current's right child has a left child, replace current with current's
            //          right child's left-most descendent
            else
            {
                // We first need to find the right node's left-most child
                BiTree<T> leftmost = current.rightNode.leftNode, 
                lmParent = current.rightNode;

                while (leftmost.leftNode != null)
                {
                    lmParent = leftmost;
                    leftmost = leftmost.leftNode;
                }

                // the parent's left subtree becomes the leftmost's right subtree
                lmParent.leftNode = leftmost.rightNode;

                // assign leftmost's left and right to current's left and right children
                leftmost.leftNode = current.leftNode;
                leftmost.rightNode = current.rightNode;

                if (childParent == null)
                    current = leftmost;
                else
                {
                    result = defComp.Compare(childParent.value, current.value);
                    if (result >= 0)
                        // parent.Value > current.Value, so make leftmost a left child of parent
                        childParent.leftNode = leftmost;
                    else if (result < 0)
                        // parent.Value < current.Value, so make leftmost a right child of parent
                        childParent.rightNode = leftmost;
                }
            }

            this.count = Recount(this);
            if (ItemRemoved != null)
                ItemRemoved(current, new TreeItemActionEventArgs<T>(TreeAction.IsDeleted));
            return true;
        }
        public IEnumerator<T> GetEnumerator()
        { 
            if (this.leftNode != null)
            {
                foreach (var child in this.leftNode)
                {
                    yield return child;
                }
            }

            yield return this.value;

            if (this.rightNode != null)
            {
                foreach (var child in this.rightNode)
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
