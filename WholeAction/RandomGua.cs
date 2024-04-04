using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ns
{
    ///<summary>
    ///
    ///<summary>
    public class RandomGua 
    {
        public static RandomGua Instance;
        public static RandomGua GetInstance()
        {
            if (Instance == null)
            {
                Instance = new RandomGua();
            }
            return Instance;
        }
        private Array guaArray;
        public Gua GetRandomGua()
        {
            guaArray = Enum.GetValues(typeof(Gua));
            int randomIndex = UnityEngine.Random.Range(0, guaArray.Length);
            return (Gua)guaArray.GetValue(randomIndex);
        }
    }
}

