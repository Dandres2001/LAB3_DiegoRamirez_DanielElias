using System;
using System.Collections.Generic;
using System.Text;

namespace LIbreriaRD
{
    public class Node<T>
    {
        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }
        public T Data { get; set; }
        public Node(T data, Node<T> next, Node<T> previous)
        {
            this.Data = data;
            this.Next = next;
            this.Previous = previous;
        }

    }
}
