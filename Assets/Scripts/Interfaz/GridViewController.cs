using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Controla la deteccion del movimiento del input del dedo. Script extraido de Internet.
/// </summary>

[AddComponentMenu("Scripts/ESI/Input/Grid View Controller")]

public class GridViewController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    #region FIELDS
    public static GridViewController Instance;
    private Grid grid;

    public enum DraggedDirection
    {
        None,
        Up,
        Down,
        Right,
        Left
    }
    public DraggedDirection currentSwipe;
    #endregion
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #region  IDragHandler - IEndDragHandler
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("Press position + " + eventData.pressPosition);
        //Debug.Log("End position + " + eventData.position);
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        //Debug.Log("norm + " + dragVectorDirection);
        GetDragDirection(dragVectorDirection);
    }

    //It must be implemented otherwise IEndDragHandler won't work 
    public void OnDrag(PointerEventData eventData)
    {

    }

    public DraggedDirection GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        DraggedDirection draggedDir;
        if (positiveX > positiveY)
        {
            //draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
            if (dragVector.x > 0)
            {
                draggedDir = DraggedDirection.Right;
                currentSwipe = DraggedDirection.Right;
            }
            else
            {
                draggedDir = DraggedDirection.Left;
                currentSwipe = DraggedDirection.Left;
            }
               
        }
        else if (positiveX <= positiveY)
        {
            //draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
            if (dragVector.y > 0)
            {
                draggedDir = DraggedDirection.Up;
                currentSwipe = DraggedDirection.Up;
            }
            else
            {
                draggedDir = DraggedDirection.Down;
                currentSwipe = DraggedDirection.Down;
            }
           
        }
        else
        {
            currentSwipe = DraggedDirection.None;
            draggedDir = DraggedDirection.None;
        }
        Debug.Log(draggedDir);
        return draggedDir;
    }
    #endregion

}
