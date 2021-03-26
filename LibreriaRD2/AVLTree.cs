using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaRD2
{


    public class AVLTree<T> where T : IComparable
        {
            public AVLTreeNode<T> Root { get; internal set; }

            #region Add items
    
            public void Add(T value)
            {
                AddTo(value, Root);
            }

            private void AddTo(T value, AVLTreeNode<T> current)
            {
                if (Root == null)
                {
                    Root = new AVLTreeNode<T>(value, null, this);
                    return;
                }
                if (current.CompareTo(value) < 0)
                {
                    if (current.Left == null)
                    {
                        current.Left = new AVLTreeNode<T>(value, current, this);
                    }
                    else
                    {
                        AddTo(value, current.Left);
                    }
                }
                else
                {
                    if (current.Right == null)
                    {
                        current.Right = new AVLTreeNode<T>(value, current, this);
                    }
                    else
                    {
                        AddTo(value, current.Right);
                    }
                }

                var parent = current;
                while (parent != null)
                {
                    if (parent.State != TreeState.Balanced)
                    {
                        parent.Balance();
                    }

                    parent = parent.Parent; //keep going up
                }

            }
            #endregion
            #region Remove
            public bool Remove(T value)
            {
                AVLTreeNode<T> current, parent = Root ;

                current = find(value, parent);

                bool removeSucessfull = RemoveNode(current);

                if (removeSucessfull)
                {
                    while (parent != null)
                    {
                        if (parent.State != TreeState.Balanced)
                        {
                            parent.Balance();
                        }

                        parent = parent.Parent; //keep going up
                    }
                }

                return true;
            }

            private bool RemoveNode(AVLTreeNode<T> current)
            {
                if (current == null || current._tree != this)
                {
                    return false;
                }

                var parent = current.Parent;
                //Case: no right child

                if (current == Root)
                {
                    Root = null;
                }
                if (current.Right == null)
                {
                    if (parent == null)
                    {
                        Root = current.Left;
                    }
                    else
                    {
                        int compare = parent.Data.CompareTo(current.Data);
                        if (compare < 0)
                        {
                            parent.Left = current.Left;
                        }
                        else
                        {
                            parent.Right = current.Left;
                        }
                    }
                }
                //Case: the right child don't have left child
                else if (current.Right.Left == null)
                {
                    current.Right.Left = current.Left;

                    if (parent == null)
                    {
                        Root = current.Right;
                    }
                    else
                    {
                        int compare = parent.Data.CompareTo(current.Data);
                        if (compare < 0)
                        {
                            parent.Left = current.Right;
                        }
                        else
                        {
                            parent.Right = current.Right;
                        }
                    }
                }
                //Case: the right child has a left child
                else
                {
                    AVLTreeNode<T> leftMost = current.Right.Left;
                    AVLTreeNode<T> leftMostParent = current.Right;

                    while (leftMost.Left != null)
                    {
                        leftMostParent = leftMost;
                        leftMost = leftMostParent.Left;
                    }
                    leftMostParent.Left = leftMost.Right;
                    leftMost.Left = current.Left;
                    leftMost.Left = current.Right;

                    if (parent == null)
                    {
                        Root = leftMost;
                    }
                    else
                    {
                        int compare = parent.Data.CompareTo(current.Data);
                        if (compare < 0)
                        {
                            parent.Left = leftMost;
                        }
                        else
                        {
                            parent.Right = leftMost;
                        }
                    }
                }
                return true;
            }
            #endregion
            #region Search
          
            public AVLTreeNode<T> find(T value, AVLTreeNode<T> parent)
            {

                if (parent != null)
                {
                    if (parent.Data.CompareTo(value) == 0)
                    {
                        return parent;
                    }
                    else if (parent.Data.CompareTo(value) < 0)
                    {
                        return find(value, parent.Left);
                    }
                    else
                    {
                        return find(value, parent.Right);
                    }
                }
                return null;
            }

            private AVLTreeNode<T> FindByParent(T value, out AVLTreeNode<T> parent)
            {
                AVLTreeNode<T> current = Root;
                parent = null;

                while (current != null)
                {
                    //int compare = current.Data.CompareTo(value);

                    if (current.CompareTo(value) > 0)
                    {
                        parent = current;
                        current = current.Right;
                    }
                    else if (current.CompareTo(value) < 0)
                    {
                        parent = current;
                        current = current.Left;
                    }
                    else
                    {
                        return current;
                    }
                }

                return null;
            }
        #endregion

        public AVLTreeNode<T> remove(AVLTreeNode<T> parent, T key)
        {
            if (parent == null) { return parent; }

            if (parent.Data.CompareTo(key) < 0)
            {
                parent.Left = remove(parent.Left, key);

            }
            else if (parent.Data.CompareTo(key) > 0)
            {
                parent.Right = remove(parent.Right, key);
            }
            else
            {
                if (parent.Left == null)
                {
                    return parent.Right;
                }
                else if (parent.Right == null)
                {
                    return parent.Left;
                }
                parent.Data = minvalue(parent.Right);
                parent.Right = remove(parent.Right, parent.Data);
            }
            return parent;

        }
        private T minvalue(AVLTreeNode<T> node)
        {
            T minv = node.Data;
            while (node.Left != null)
            {
                minv = node.Left.Data;
                node = node.Left;
            }
            return minv;
        }


    }



}

