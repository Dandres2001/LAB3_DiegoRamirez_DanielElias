using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaRD2
{

    public class Nodetree<T>

    {


        public Nodetree<T> leftnode { get; set; }
        public Nodetree<T> rightnode { get; set; }
   
        public T Data { get; set; }
    }
}