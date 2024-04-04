using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ns
{
    ///<summary>
    ///
    ///<summary>
    public class SkillLogical :MonoBehaviour
    {
        public GameObject rotatePannel;
        private InitiateMap InitiateMap;
        private GameObject scriptAssemble;
        private void Start()
        {
            scriptAssemble = GameObject.Find("ScriptAssemble");
            InitiateMap = scriptAssemble.GetComponent<InitiateMap>();
        }
        public void SkillChoose(int choice)
        {
            switch (choice)
            {
                case 0:
                    ShowRepeatRandomGua();
                    break;
                case 1:
                    MoveBarrier();
                    break;
                case 2:
                    RotateMap();
                    break;
            }
        }

        private void ShowRepeatRandomGua()
        {
            Gua[] RandomGuas = RepeatRandomGua();

        }

        private void RotateMap()
        {
            rotatePannel.SetActive(true);
        }

        private void MoveBarrier()
        {
            //
        }

        private Gua[] RepeatRandomGua()
        {
            Gua[] guas = new Gua[2];
            guas[0]=RandomGua.GetInstance().GetRandomGua();
            guas[1]= RandomGua.GetInstance().GetRandomGua();
            return guas;
        }
    }
}

