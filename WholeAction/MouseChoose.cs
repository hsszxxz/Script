using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace ns
{
    ///<summary>
    ///
    ///<summary>
    public class MouseChoose
    {
        public static MouseChoose Instance;
        public static MouseChoose GetInstance()
        {
            if (Instance == null)
            {
                Instance = new MouseChoose();
            }
            return Instance;
        }

        public Transform GetHitTransform(string tag)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.transform.CompareTag(tag))
            {
                return hit.transform;
            }
            else
            {
                return null;
            }
        }
        public Transform UIGetHitTransform( GraphicRaycaster CanvasGraphicRaycaster,string tag,EventSystem eventSystem)
        {
            PointerEventData eventData = new PointerEventData(eventSystem);
            eventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            CanvasGraphicRaycaster.Raycast(eventData, results);
            if (results != null && results[0].gameObject.transform.CompareTag(tag))
            {
                return results[0].gameObject.transform;
            }
            else
            {
                return null;
            }
        }
        public void ChageSprite (Transform objectTransform, Sprite changeSprite)
        {
            if (objectTransform != null)
            {
                SpriteRenderer spriteRenderer = objectTransform.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = changeSprite;
            }
        }
         public void UIChangeSprite(Transform objectTransform, Sprite changeSprite)
        {
            if (objectTransform != null)
            {
                Image image = objectTransform.GetComponent<Image>();
                image.sprite = changeSprite;
            }
        }
    }
}

