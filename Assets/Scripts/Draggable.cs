using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    GameObject placeholder = null;

    public Transform parentToReturnTo = null;
    public Transform placeholderParent = null;
    public bool rotated = false;
    public int baseRotation = 0;
    public Button rotationButtonPrefab;
    private int rotation = 0;
    bool MouseOnObject = false;
    bool rotationButtonActive = false;
    public Manager manager;
    Player owner;
    Card cardScript;


    public void Update(){
        if(owner == null){
            owner = GetComponent<Card>().owner;
        }
        if ((Input.GetMouseButtonDown(0)) && (!MouseOnObject) && rotationButtonActive)
            {
               Debug.Log("MHANZ");
               Button button = GetComponentInChildren<Button>();
               button.gameObject.SetActive(false);
               rotationButtonActive = false;
               parentToReturnTo.GetComponent<CardSpace>().card = cardScript;
               manager.changeTurn();
            }
    }

    public void Awake(){
        this.transform.rotation = Quaternion.Euler(0,0, baseRotation);
        cardScript = GetComponent<Card>();
        manager = GameObject.FindWithTag("manager").GetComponent<Manager>();
    }

     public void OnPointerEnter(PointerEventData pointerEventData)
     {
         MouseOnObject = true;
 
     }
     public void OnPointerExit(PointerEventData pointerEventData)
     {
         MouseOnObject = false;
     }
 
    public void OnBeginDrag(PointerEventData eventData){
        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;
        placeholder.transform.SetSiblingIndex( this.transform.GetSiblingIndex() );
        
        parentToReturnTo = this.transform.parent;
        placeholderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);
    }

    public void OnDrag(PointerEventData eventData){
        this.transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        if(placeholder.transform.parent != placeholderParent){
            placeholder.transform.SetParent(placeholderParent);
        }
        int newSiblingIndex = placeholderParent.childCount;
        for(int i = 0; i<placeholderParent.childCount; i++){
            if(this.transform.position.x < placeholderParent.GetChild(i).position.x){
                newSiblingIndex = i;
                if(placeholder.transform.GetSiblingIndex() < newSiblingIndex){
                    newSiblingIndex--;
                }
                break;
            }
        }
        placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData){
        this.transform.rotation = Quaternion.Euler(0,0, baseRotation);
        if(parentToReturnTo.tag == "spazio_down"){
            this.transform.rotation = Quaternion.Euler(0,0,180);
        }
        this.transform.SetParent(parentToReturnTo);
        if(this.transform.parent.tag == "mano"){
            this.transform.rotation = Quaternion.Euler(0,0,0);
        }else{
            if(!rotationButtonActive){
                baseRotation = AskRotation();
            }
        }
        this.transform.SetSiblingIndex( placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(placeholder);
    }

    int AskRotation(){
        rotationButtonActive = true;
        Canvas canvas = GetComponentInParent<Canvas>();
        Vector3 buttonPosition = this.transform.position;
        buttonPosition.y += 50;
        CreateButton(this.transform, buttonPosition, new Vector2(50,50), EventRotation);
        return 0;
    }

    void EventRotation(){
        rotation += 1;
        if(rotation > 2){
            rotation = 0;
        }
        Quaternion actualRotation = this.transform.rotation;
        actualRotation.z += 120;
        if(!rotated){
            StartCoroutine( Rotate( new Vector3(actualRotation.x, actualRotation.y, 120*rotation), 0.1f ) ) ;
        } else{
            StartCoroutine( Rotate( new Vector3(actualRotation.x, actualRotation.y, 180 + 120*rotation), 0.1f ) ) ;
        }
        cardScript.UpdateRotation();
        cardScript.stampaValori();
    }

    public void CreateButton(Transform panel ,Vector3 position, Vector2 size, UnityEngine.Events.UnityAction method)
    {
        Button rotationButton = Instantiate(rotationButtonPrefab, position, Quaternion.identity) as Button;
        rotationButton.transform.SetParent(panel);
        rotationButton.transform.localPosition = new Vector3(0, 0, 0);
        rotationButton.GetComponent<RectTransform>().sizeDelta = size;
        rotationButton.GetComponent<Button>().onClick.AddListener(method);
        rotationButton.gameObject.SetActive(true);
    }

    public void UpdateRotation() {
        if(rotated){
            StartCoroutine( Rotate( new Vector3(0, 0, baseRotation + 180), 0.1f ) ) ;
            //this.transform.rotation = Quaternion.Euler(0,0,180);
        } else{
            //this.transform.rotation = Quaternion.Euler(0,0,0);
            StartCoroutine( Rotate( new Vector3(0, 0, baseRotation), 0.1f ) ) ;
        }
    }


    private IEnumerator Rotate( Vector3 angles, float duration )
    {
        //rotating = true ;
        Quaternion startRotation = this.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(angles);
        //Quaternion endRotation = Quaternion.Euler( angles ) * startRotation;
        for( float t = 0 ; t < duration ; t+= Time.deltaTime )
        {
            this.transform.rotation = Quaternion.Lerp( startRotation, endRotation, t / duration );
            yield return null;
        }
        this.transform.rotation = endRotation;
        //rotating = false;
    }
}
