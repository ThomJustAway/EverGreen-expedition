using Patterns;
using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ImageDragable : Singleton<ImageDragable>  , IPointerUpHandler, IDragHandler
    {
        [SerializeField] private Image imageComponent;
        [SerializeField] private Canvas canvas;
        private Turret assignTurret;
        private bool startMoving = false;
        public void OnDrag(PointerEventData eventData)
        {
            MoveImage();
        }

        public void StartShowingMovingImage(Turret turretAssign)
        {
            assignTurret = turretAssign;
            startMoving = true;
            gameObject.SetActive(startMoving);//now show the image 
        }

        private void StopMoving()
        {
            startMoving = false;
            gameObject.SetActive(startMoving);
        }

        private void Update()
        {
            if (startMoving)
            {
                MoveImage();
            }

            if(Input.GetMouseButtonDown(0))
            {
                StopMoving();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //create the prefab here
        }

        private Vector3 GetMousePositionInWorldSpace()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        private void MoveImage()
        {
            var newPosition = Input.mousePosition / canvas.scaleFactor;
            newPosition.x -= Screen.width / 2;
            newPosition.y -= Screen.height / 2;
            imageComponent.rectTransform.anchoredPosition = newPosition;
        }
    }
}