using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaRD2
{


        public class AVLTree<T> where T : IComparable
        {
            public AVLTreeNode<T> Head { get; internal set; }

            #region Add items

            public void Add(T input)
            {
                AddTo(input, Head);
            }

            private void AddTo(T input, AVLTreeNode<T> current)
            {
                if (Head == null)
                {
                    Head = new AVLTreeNode<T>(input, null, this);
                    return;
                }
                if (input.CompareTo(current.Value) < 0)
                {
                    if (current.Left == null)
                    {
                        current.Left = new AVLTreeNode<T>(input, current, this);
                    }
                    else
                    {
                        AddTo(input, current.Left);
                    }
                }
                else
                {
                    if (current.Right == null)
                    {
                        current.Right = new AVLTreeNode<T>(input, current, this);
                    }
                    else
                    {
                        AddTo(input, current.Right);
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
            public bool Remove(T input)
            {
                AVLTreeNode<T> current, parent;

                current = FindWithParent(input, out parent);

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

                if (current == Head)
                {
                    Head = null;
                }
                if (current.Right == null)
                {
                    if (parent == null)
                    {
                        Head = current.Left;
                    }
                    else
                    {
                        int compare = parent.Value.CompareTo(current.Value);
                        if (compare > 0)
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
                        Head = current.Right;
                    }
                    else
                    {
                        int compare = parent.Value.CompareTo(current.Value);
                        if (compare > 0)
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
                        Head = leftMost;
                    }
                    else
                    {
                        int compare = parent.Value.CompareTo(current.Value);
                        if (compare > 0)
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
            public bool Search(T input)
            {
                return SearchNode(input, Head);
            }
            private bool SearchNode(T input, AVLTreeNode<T> current)
            {
                if (current == null)
                {
                    return false;
                }

                if (input.CompareTo(current.Value) == 0)
                {
                    return true;
                }

                return SearchNode(input,
                    input.CompareTo(current.Value) < 0
                        ? current.Left
                        : current.Right);
            }
            public AVLTreeNode<T> find(T value, AVLTreeNode<T> parent)
            {

                if (parent != null)
                {
                    if (parent.Value.CompareTo(value) == 0)
                    {
                        return parent;
                    }
                    if (parent.Value.CompareTo(value) < 0)
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

            private AVLTreeNode<T> FindWithParent(T input, out AVLTreeNode<T> parent)
            {
                AVLTreeNode<T> current = Head;
                parent = null;

                while (current != null)
                {
                    int compare = current.Value.CompareTo(input);

                    if (compare > 0)
                    {
                        parent = current;
                        current = current.Left;
                    }
                    else if (compare < 0)
                    {
                        parent = current;
                        current = current.Right;
                    }
                    else
                    {
                        return current;
                    }
                }

                return null;
            }
            #endregion



        }

    }

