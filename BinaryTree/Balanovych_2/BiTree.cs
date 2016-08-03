using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanovych_2
{
    class BiTree<T> : IEnumerable<T> /*where T : IComparable<T>*/
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
            if (this.value.Equals(value))
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
        public void Remove(T value)
        {
            if (this.IsEmpty()) { // can't delete from an empty tree
    		    //throw new NoSuchElementException();
                return;
    	    }
            BiTree<T> t = Search(value);
            if (t==null)
            {
                throw new Exception("Нет такого элемента для удаления");
            }

            /*string[] str1 = Display(t).TrimEnd().Split(' ');
            string[] str2 = new string[str1.Length - 1];

            int i = 0;
            foreach (string s in str1)
            {
                if (s != value)
                    str2[i++] = s;
            }

            t.Clear();
            foreach (string s in str2)
                t.Insert(s);*/

            this.count = Recount(this);

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
