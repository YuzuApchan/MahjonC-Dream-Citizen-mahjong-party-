using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*using UnityEngine;*/


public class Group
{       
        /*相同的牌称为一组*/
        private readonly int loc = 0;
        private int len = 1;
        private int comfirmed = 0;
        //  public int Loc => loc;
        public int Loc => loc;
        public int Len { get => len; set => len = value; }
        public int Compirmed { get => comfirmed; set => comfirmed = value; }


        public Group(int _loc) => loc = _loc;
    
}
