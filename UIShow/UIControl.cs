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
    public class UIControl : MonoBehaviour
    {
        private UIlogic UIlogicScript;
        private GraphicRaycaster graphicRaycaster;
        private EventSystem eventSystem;
        private void Start()
        {
            UIlogicScript = GetComponent<UIlogic>();
            graphicRaycaster = UIlogicScript.graphicRaycaster;
            eventSystem = UIlogicScript.eventSystem;
        }
        private void Update()
        {
            UIlogicScript.BloodComAndShow();
            UIlogicScript.SkillPositionControl();
            Transform chosenGuaTransform = MouseChoose.GetInstance().UIGetHitTransform(graphicRaycaster,"Gua",eventSystem);
            if (chosenGuaTransform != null)
            {
                ChooseGua(chosenGuaTransform);
            }
        }

        private void ChooseGua(Transform chosenGuaTransform)
        {
            SpriteRenderer spriteRenderer = chosenGuaTransform.GetComponent<SpriteRenderer>();
            Sprite guaSprite = spriteRenderer.sprite;
            spriteRenderer.color = Color.red;
            if (Input.GetMouseButton(0))
            {
                MouseChoose.GetInstance().UIChangeSprite(UIlogicScript.chosenGua.transform, guaSprite);
                UIlogicScript.chosenGuaRectTransform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                MouseChoose.GetInstance().UIChangeSprite(UIlogicScript.chosenGua.transform,  null);
            }
        }
    }
}

