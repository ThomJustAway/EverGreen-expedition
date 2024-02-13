using Patterns;
using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ImageDragable : Singleton<ImageDragable> , IDragHandler
    {
        [SerializeField] private Image imageComponent;
        [SerializeField] private Canvas canvas;
        private Turret assignTurret;
        [SerializeField]private Transform turretContainer;
        private bool startMoving = false;
        public void OnDrag(PointerEventData eventData)
        {
            MoveImage();
        }

        public void StartShowingMovingImage(Turret turretAssign)
        {
            assignTurret = turretAssign;
            imageComponent.sprite = turretAssign.TurretSprite;
            startMoving = true;

            gameObject.SetActive(startMoving);//now show the image 
        }

        protected override void Awake()
        {
            base.Awake();
            gameObject.SetActive(false); //do not show it at the player
        }

        private void Update()
        {
            if (startMoving)
            {
                MoveImage();
            }

            if(Input.GetMouseButtonDown(0))
            {//place the turret down
                StopMoving();
            }
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
        private void StopMoving()
        {
            startMoving = false;
            gameObject.SetActive(startMoving);

            SoundManager.Instance.PlayAudio(SFXClip.PlacingTurret);
            //place the turret down
            GameObject turret = Instantiate(assignTurret.gameObject , turretContainer);
            Vector3 newPosition = GetMousePositionInWorldSpace();
            newPosition.z = 0;
            turret.transform.localPosition = newPosition;

        }
    }
}