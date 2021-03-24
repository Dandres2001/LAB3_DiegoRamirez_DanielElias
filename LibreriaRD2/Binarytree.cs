using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaRD2
{
    public class Binarytree<T> where  T:IComparable
    {

        public Nodetree<T> root { get; set; }

        public Nodetree<T> before { get; set; }
       public Nodetree<T> after { get; set; }

        public Nodetree<T> info { get; set; }
        public bool insert(T value)
        {
             before = null;  
           after = this.root;
            
            while (after != null)
            {
                before = after;
           
                if (after.Data.CompareTo(value)< 0)
                {
                    after = after.leftnode;
                }
                else if (after.Data.CompareTo(value) > 0)
                {
                    after = after.rightnode;
                }
                else
                {
                    return false;
                }
            }
            Nodetree<T> newnode = new Nodetree<T>();
            newnode.Data = value;
            if (this.root == null)
            {
                this.root = newnode;
            }
            
            else
            {
                if (before.Data.CompareTo(value) <0)
                {
                    before.leftnode = newnode;
              
                }
                else
                {
                    before.rightnode = newnode;
           
                }
            }

            return true;
        }


        public Nodetree<T> find(T value, Nodetree<T> parent)
        {

            if (parent != null)
            {
                if (parent.Data.CompareTo(value) == 0)
                {
                    return parent;
                }
                if (parent.Data.CompareTo(value) < 0)
                {
                    return find(value, parent.leftnode);
                }
                else
                {
                    return find(value, parent.rightnode);
                }
            }
            return null;
        }

        public Nodetree<T> remove(Nodetree<T> parent , T key)
        {
            if (parent == null) { return parent; }

            if (parent.Data.CompareTo(key) < 0)
            {
                parent.leftnode = remove(parent.leftnode, key); 

            }
            else  if (parent.Data.CompareTo(key) > 0)
            {
                parent.rightnode = remove(parent.rightnode, key);
            }
            else
            {
                if  (parent.leftnode == null)
                {
                    return parent.rightnode;
                }
                else  if (parent.rightnode == null)
                {
                    return parent.leftnode;
                }
                parent.Data = minvalue(parent.rightnode);
                parent.rightnode = remove(parent.rightnode, parent.Data);
            }
            return parent;

        }

    
       
      

        private T minvalue(Nodetree<T> node)
        {
           T minv = node.Data;
            while (node.leftnode!= null)
            {
                minv = node.leftnode.Data;
                node = node.leftnode;
            }
            return minv;
        }

      
    }  

}

