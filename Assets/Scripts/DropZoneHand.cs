using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZoneHand : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Player player;

    void Awake(){
        foreach (Card c in GetComponentsInChildren<Card>()){
            c.owner = player;
        }
    }

    public void OnDrop(PointerEventData eventData){
        Debug.Log("OnDrop to" + gameObject.name);

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if(d!=null){
            d.parentToReturnTo = this.transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData){
        if(eventData.pointerDrag == null){
            return;
        }
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if(d!=null){
            d.placeholderParent = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData){
        if(eventData.pointerDrag == null){
            return;
        }
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if(d!=null && d.placeholderParent==this.transform){
            d.placeholderParent = d.parentToReturnTo;
        }
    }

}
