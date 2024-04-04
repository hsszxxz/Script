using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
namespace ns
{
    public enum RoundState
    {
        Character,
        Enemy,
    }
    public class RoundControl : MonoBehaviour
    {
        [ReadOnly(true)]
        public int characterCount=3;
        [ReadOnly(true)]
        public RoundState roundState;
        private GameObject eventSystem;
        private void Start()
        {
            eventSystem = GameObject.Find("EventSystem");
        }
        private void Update()
        {
            CharacterCountCharge();
            switch(roundState)
            {
                case RoundState.Character:
                    eventSystem.SetActive(true);
                    break;
                case RoundState.Enemy:
                    eventSystem.SetActive(false);
                    break;
            }
        }
        void CharacterCountCharge()
        {
            if (characterCount == 0)
            {
                roundState = RoundState.Enemy;
                characterCount = 3;
            }
        }
    }
}

