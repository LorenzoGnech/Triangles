using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZoneHand : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
