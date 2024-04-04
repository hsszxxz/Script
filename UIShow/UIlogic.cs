using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace ns
{
    ///<summary>
    ///
    ///<summary>
    public class UIlogic : MonoBehaviour
    {
        [HideInInspector]
        public LuBanProperties banProperties;

        [Header ("GuaShow")]
        public Sprite[] guaSpriteAssemble;
        public GameObject[] randomGuaImagePosition;
        public GameObject chosenGua;
        public Transform skillImagePosition;

        [Header("BloodBarShow")]
        public Scrollbar bloodBar;

        [Header("SkillButton(随机，移动障碍物，旋转)")]
        public Button[] skillButtons;
        public Button confirm;
        public Button cancel;
        public GameObject skillButtonAssemble;
        public GameObject confirmButtonAssemble;

        [Header("EventSystem")]
        public EventSystem eventSystem;

        public SkillLogical skillLogical;
        [HideInInspector]
        public SpriteRenderer chosenGuaSpriteRenderer;
        [HideInInspector]
        public GraphicRaycaster graphicRaycaster;
        [HideInInspector]
        public RectTransform chosenGuaRectTransform;

        public RoundControl roundControl;

        private void Start()
        {
            graphicRaycaster = GetComponent<GraphicRaycaster>();
            chosenGuaRectTransform = chosenGua.GetComponent<RectTransform>();
            skillButtons[0].onClick.AddListener(ActionControlRandomGua);
            skillButtons[1].onClick.AddListener(ActionControlRemove);
            skillButtons[2].onClick.AddListener(ActionControlRotate);
            confirm.onClick.AddListener(ActionConfirm);
            cancel.onClick.AddListener(ActionCancel);
        }
        public void SkillPositionControl()
        {
            if (Vector3.Distance(skillImagePosition.transform.position, chosenGua.transform.position) <= 0.5f)
            {
                MouseChoose.GetInstance().UIChangeSprite(skillImagePosition, chosenGua.GetComponent<Sprite>());
            }
        }

        public void BloodComAndShow()
        {
            bloodBar.size = (float)banProperties.currentBlood / (float)banProperties.maxBlood;
        }
        public void ShowGuaPicture(Gua gua, GameObject randomGua)
        {
            Image randomGuaImage = randomGua.GetComponent<Image>();
            switch (gua)
            {
                case Gua.Qian:
                    randomGuaImage.sprite = guaSpriteAssemble[0]; break;
                case Gua.Dui:
                    randomGuaImage.sprite = guaSpriteAssemble[1]; break;
                case Gua.Li:
                    randomGuaImage.sprite = guaSpriteAssemble[2]; break;
                case Gua.Zhen:
                    randomGuaImage.sprite = guaSpriteAssemble[3]; break;
                case Gua.Xun:
                    randomGuaImage.sprite = guaSpriteAssemble[4]; break;
                case Gua.Kan:
                    randomGuaImage.sprite = guaSpriteAssemble[5]; break;
                case Gua.Gen:
                    randomGuaImage.sprite = guaSpriteAssemble[6]; break;
                case Gua.Kun:
                    randomGuaImage.sprite = guaSpriteAssemble[7]; break;
            }
        }

        private void ActionControlRandomGua()
        { 
            skillLogical.SkillChoose(0);
            ControlSwtich(true);
        }

        private void ActionControlRemove()
        {
            skillLogical.SkillChoose(1);
            ControlSwtich(true);
        }

        private void ActionControlRotate()
        {
            skillLogical.SkillChoose(2);
            ControlSwtich(true);
        }

        private void ActionConfirm()
        {
            roundControl.characterCount += 1;
            ControlSwtich(false);
        }

        private void ActionCancel()
        {
            roundControl.characterCount -=1;
            ControlSwtich(false) ;
        }

        private void ControlSwtich(bool isSwitch)
        {
            bool flag = false;
            if (isSwitch)
            {
                flag = true;
            }
            skillButtonAssemble.SetActive(!flag);
            confirmButtonAssemble.SetActive(flag);
        }
    }
}

