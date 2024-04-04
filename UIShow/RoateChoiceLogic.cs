using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ns
{
    ///<summary>
    ///
    ///<summary>
    public class RoateChoiceLogic : MonoBehaviour
    {
        private bool direction;
        private int circle;
        private int steps =0;
        public Button directionShunButton;
        public Button directionNiButton;
        public Button zuiWaiButton;
        public Button zhongButton;
        public Button ZuiNeiButton;
        public Button Plus;
        public Button Minues;
        public Button confirm;
        public Text countText;

        private GameObject scriptAssemble;
        private InitiateMap InitiateMap;
        private void Start()
        {
            scriptAssemble = GameObject.Find("ScriptAssemble");
            InitiateMap = scriptAssemble.GetComponent<InitiateMap>();
            directionShunButton.onClick.AddListener(Shun);
            directionNiButton.onClick.AddListener(Ni);
            zuiWaiButton.onClick.AddListener(Wai);
            zhongButton.onClick.AddListener(Zhong);
            ZuiNeiButton.onClick.AddListener(Nei);
            Plus.onClick.AddListener(PlusMethod);
            Minues.onClick.AddListener(MinusMethod);
            confirm.onClick.AddListener(Confirm);
        }
        private void Shun()
        {
            directionNiButton.image.color = Color.white;
            directionShunButton.image.color = Color.red;
            direction = true;
        }
        private void Ni()
        {
            directionShunButton.image.color = Color.white;
            directionNiButton.image.color = Color.red;
            direction = false;
        }
        private void Wai()
        {
            zuiWaiButton.image.color = Color.red;
            ZuiNeiButton.image.color = Color.white;
            zhongButton.image.color = Color.white;
            circle = 0;
        }
        private void Zhong()
        {
            zuiWaiButton.image.color = Color.white;
            ZuiNeiButton.image.color = Color.white;
            zhongButton.image.color = Color.red;
            circle = 1;
        }
        private void Nei()
        {
            zuiWaiButton.image.color = Color.white;
            ZuiNeiButton.image.color = Color.red;
            zhongButton.image.color = Color.white;
            circle = 2;
        }
        private void PlusMethod()
        {
            steps += 1;
            countText.text = "步数："+steps.ToString();
        }
        private void MinusMethod()
        {
            steps -= 1;
            countText.text = "步数：" + steps.ToString();
        }
        private void Confirm()
        {
            InitiateMap.RotateMap(direction,circle ,steps);
            confirm.onClick.RemoveListener(Confirm);
            this.gameObject.SetActive(false);
        }
    }
}

