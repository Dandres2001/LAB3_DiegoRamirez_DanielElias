using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaRD2
{

    public class AVLTreeNode<T> where T : IComparable
    {
 


        private AVLTreeNode<T> _left;
        private AVLTreeNode<T> _right;
        internal AVLTree<T> _tree;

        public AVLTreeNode(T value, AVLTreeNode<T> parent, AVLTree<T> tree)
        {
            Data = value;
            Parent = parent;
            _tree = tree;
        }

    
        public T Data { get; set; }

        public AVLTreeNode<T> Left
        {
            get { return _left; }
            set
            {
                _left = value;
                if (_left != null)
                {
                    _left.Parent = this;
                }
            }
        }
        public AVLTreeNode<T> Right
        {
            get { return _right; }
            set
            {
                _right = value;
                if (_right != null)
                {
                    _right.Parent = this;
                }
            }
        }
        public AVLTreeNode<T> Parent { get; set; }

        public int CompareTo(T other)
        {
            return Data.CompareTo(other);
        }

        internal void Balance()
        {
            if (State == TreeState.RightHeavy)
            {
                if (Right != null && Right.BalanceFactor < 0)
                {

                    LeftRightRotation();
                }
                else
                {
                    LeftRotation();
                }
            }
            else if (State == TreeState.LeftHeavy)
            {
                if (Left != null && Left.BalanceFactor > 0)
                {

                    RightLeftRotation();
                }
                else
                {
                    RightRotation();


                }
            }
        }
        private void LeftRotation()
        {
            AVLTreeNode<T> rootParent = Parent;
            AVLTreeNode<T> root = this;
            AVLTreeNode<T> temp = Right;

            bool isLeftChild = (rootParent != null) && rootParent.Left == root;
            root.Right = temp.Left;
            temp.Left = root;

            root.Parent = temp;
            temp.Parent = rootParent;

            if (root.Right != null)
                root.Right.Parent = root;

            if (_tree.Root == root)
            {
                _tree.Root = temp;
            }
            else if (isLeftChild)
            {
                rootParent.Left = temp;
            }
            else if (rootParent != null)
            {
                rootParent.Right = temp;
            }
        }
        private void RightRotation()
        {
            AVLTreeNode<T> rootParent = Parent;
            AVLTreeNode<T> root = this;
            AVLTreeNode<T> temp = Left;
            bool isLeftChild = (rootParent != null) && rootParent.Left == root;

            root.Left = temp.Right;
            temp.Right = root;

            root.Parent = temp;
            temp.Parent = rootParent;

            if (root.Left != null)
                root.Left.Parent = root;

            if (_tree.Root == root)
            {
                _tree.Root = temp;
            }
            else if (isLeftChild)
            {
                rootParent.Left = temp;
            }
            else if (rootParent != null)
            {
                rootParent.Right = temp;
            }
        }
        private void LeftRightRotation()
        {
            Right.RightRotation();
            LeftRotation();
        }
        private void RightLeftRotation()
        {
            Left.LeftRotation();
            RightRotation();
        }
        private int MaxHeight(AVLTreeNode<T> node)
        {
            if (node != null)
            {
                return 1 + Math.Max(MaxHeight(node.Left), MaxHeight(node.Right));
            }
            return 0;
        }
        private int LeftHeight { get { return MaxHeight(Left); } }
        private int RightHeight { get { return MaxHeight(Right); } }
        public TreeState State
        {
            get
            {
                if (LeftHeight - RightHeight > 1)
                {
                    return TreeState.LeftHeavy;
                }

                if (RightHeight - LeftHeight > 1)
                {
                    return TreeState.RightHeavy;
                }

                return TreeState.Balanced;
            }
        }
        private int BalanceFactor
        {
            get { return RightHeight - LeftHeight; }
        }


    }
}