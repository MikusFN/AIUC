using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Hexa
{
   public class Wall
    {
        public GameObject prefab;
        public bool drawn = false;
        
        public Wall()
        {
            drawn = true;
        }
    }
}
