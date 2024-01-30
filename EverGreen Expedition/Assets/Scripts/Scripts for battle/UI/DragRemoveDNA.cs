using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragRemoveDNA : MonoBehaviour ,
    IBeginDragHandler,
  IDragHandler,
  IEndDragHandler
{


    [SerializeField]
    RectTransform UIDragElement;

    [SerializeField]
    RectTransform Canvas;

    private Vector2 mOriginalLocalPointerPosition;
    private Vector3 mOriginalPanelLocalPosition;
    private Vector2 mOriginalPosition;

    private void Start()
    {
        mOriginalPosition = UIDragElement.localPosition;
    }


    public void OnBeginDrag(PointerEventData data)
    {
        mOriginalPanelLocalPosition = UIDragElement.localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
          Canvas,
          data.position,
          data.pressEventCamera,
          out mOriginalLocalPointerPosition);

    }

    public void OnDrag(PointerEventData data)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
          Canvas,
          data.position,
          data.pressEventCamera,
          out localPointerPosition))
        {
            Vector3 offsetToOriginal =
              localPointerPosition -
              mOriginalLocalPointerPosition; //find the offset from the mouse new position from the old position

            UIDragElement.localPosition =
              mOriginalPanelLocalPosition +
              offsetToOriginal; //update the local position
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(
          Coroutine_MoveUIElement(
            UIDragElement,
            mOriginalPosition,
            0.5f));

        
        Vector2 hitPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.Raycast(hitPosition, Vector2.zero, 100f, LayerMaskManager.TurretLayerMask);
        if(hit.collider != null)
        {
            //it is a plant
            Turret turretComponent;
            if (hit.transform.TryGetComponent<Turret>(out turretComponent))
            {
                turretComponent.RemoveTurret();
            }
        }

        
    }

    public IEnumerator Coroutine_MoveUIElement(
  RectTransform r,
  Vector2 targetPosition,
  float duration = 0.1f)
    {
        float elapsedTime = 0;
        Vector2 startingPos = r.localPosition;
        while (elapsedTime < duration)
        {
            r.localPosition =
              Vector2.Lerp(
                startingPos,
                targetPosition,
                (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        r.localPosition = targetPosition;
    }



}
