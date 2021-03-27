using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaRD2
{


    public class AVLTree<T> where T : IComparable
        {
            public AVLTreeNode<T> Root { get; internal set; }

         
    
       

            public void AddTo(T value, AVLTreeNode<T> current)
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
                    if (parent.State != BalanceState.Balanced)
                    {
                        parent.Balance();
                    }

                    parent = parent.Parent; 
                }

            }
     
        
            public bool Remove(T value)
            {
                AVLTreeNode<T> current, parent;

                current =FindByParent(value,out parent );

        
            remove(Root, value);
         
                    while (parent != null)
                    {
                        if (parent.State != BalanceState.Balanced)
                        {
                            parent.Balance();
                        }

                        parent = parent.Parent; 
                    }
                

                return true;
            }

         
      
        
          
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
      

        private AVLTreeNode<T> remove(AVLTreeNode<T> parent, T key)
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

