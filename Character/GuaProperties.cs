using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ns
{
    public enum FiveElements
    {
        Jin,
        Mu,
        Shui,
        Huo,
        Tu
    }
    public enum Gua
    {
        Qian,
        Dui,
        Li,
        Zhen,
        Xun,
        Kan,
        Gen,
        Kun
    }
    public class GuaProperties : MonoBehaviour
    {
        public Dictionary<Gua, object> guaFiveElements;
        public Dictionary<Gua, Vector3> guaVectors;
        public Dictionary <Gua, Vector3[]> guaAttackRange;
        
        void Start ()
        {
            SetUpGuaFiveElements();
            SetUpGuaVectors();
            SetUpGuaAttackRange();
        }
        private void  SetUpGuaFiveElements()
        {
            guaFiveElements = new Dictionary<Gua, object>();
            guaFiveElements[Gua.Qian] = "Jian";
            guaFiveElements[Gua.Dui] = FiveElements.Jin;
            guaFiveElements[Gua.Li] = FiveElements.Huo;
            guaFiveElements[Gua.Zhen] = FiveElements.Mu;
            guaFiveElements[Gua.Xun] = "Ru";
            guaFiveElements[Gua.Kan] = FiveElements.Shui;
            guaFiveElements[Gua.Gen] = "Zhi";
            guaFiveElements[Gua.Kun] = FiveElements.Tu;
        }
        private void SetUpGuaVectors()
        {
            guaVectors = new Dictionary<Gua, Vector3>();
            guaVectors[Gua.Qian] = new Vector3(1,1,1);
            guaVectors[Gua.Dui] = new Vector3(0, 1, 1);
            guaVectors[Gua.Li] = new Vector3(1, 0, 1);
            guaVectors[Gua.Zhen] = new Vector3(0, 0, 1);
            guaVectors[Gua.Xun] = new Vector3(1, 1, 0);
            guaVectors[Gua.Kan] = new Vector3(0, 1, 0);
            guaVectors[Gua.Gen] = new Vector3(1, 0, 0);
            guaVectors[Gua.Kun] = new Vector3(0, 0, 0);
        }
        private void SetUpGuaAttackRange()
        {
            guaAttackRange = new Dictionary<Gua, Vector3[]>();
            guaAttackRange[Gua.Qian] = new Vector3[3];
            guaAttackRange[Gua.Dui] = new Vector3[3];
            guaAttackRange[Gua.Li] = new Vector3[3];
            guaAttackRange[Gua.Zhen] = new Vector3[3];
            guaAttackRange[Gua.Xun] = new Vector3[3];
            guaAttackRange[Gua.Kan] = new Vector3[3];
            guaAttackRange[Gua.Gen] = new Vector3[3];
            guaAttackRange[Gua.Kun] = new Vector3[3];
        }
     
    }
}

