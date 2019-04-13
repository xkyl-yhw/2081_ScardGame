using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class PuzzleManager : MonoBehaviour,IPointerUpHandler,IEndDragHandler,IDragHandler,IPointerDownHandler {

    private GraphicRaycaster UIGraphicRaycaster;
    private EventSystem UIEventSystem;
    [SerializeField]
    private RectTransform Map;
    List<RaycastResult> raycastresults = new List<RaycastResult>();

    private Vector2 offset=Vector2.zero;
    private GameObject targetPuzzle;

    private void Start()
    {
        UIGraphicRaycaster = GetComponent<GraphicRaycaster>();
        UIEventSystem = GetComponent<EventSystem>();
    }
    /*
    private void Update()
    {
        var mPointEventDate = new PointerEventData(UIEventSystem);
        if (Input.GetMouseButtonDown(0))
        {
            
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log(1);
            }
            else Debug.Log(2);
            
            raycastresults = GraphicRaycaster(Input.mousePosition);
            ChangeUI();
            OnPointerDown(mPointEventDate);
        }
        if (Input.GetMouseButton(0))
        {
            OnDrag(mPointEventDate);   
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnEndDrag(mPointEventDate);
        }
    }
    */

    public void OnPointerUp(PointerEventData eventData)
    {
        offset = Vector2.zero;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        offset = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 MousePosition = eventData.position;
        Vector2 mouseUguiPos = new Vector2();
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(Map, MousePosition, eventData.enterEventCamera,out mouseUguiPos);
        if (isRect&&raycastresults.Count>2)
        {
            Map.transform.GetChild(Map.transform.childCount-1).GetComponent<RectTransform>().anchoredPosition = offset + mouseUguiPos;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        raycastresults = GraphicRaycaster(Input.mousePosition);
        ChangeUI();
        Vector2 MousePosition = eventData.position;
        Vector2 mouseUguiPos = new Vector2();
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(Map, MousePosition, eventData.enterEventCamera,out mouseUguiPos);
        if (isRect&&raycastresults.Count>2)
        {
            offset = Map.transform.GetChild(Map.transform.childCount - 1).GetComponent<RectTransform>().anchoredPosition - mouseUguiPos;
        }
    }

    private void ChangeUI()
    {
        Debug.Log(raycastresults.Count);
        bool isget = false;
        for (int i = raycastresults.Count-1; i >=0 ; i--)
        {
            GameObject go = raycastresults[i].gameObject;
            if (go.layer == LayerMask.NameToLayer("PuzzleBlock"))
            {
                raycastresults[i].gameObject.transform.SetAsLastSibling();
                targetPuzzle = raycastresults[i].gameObject;
            }
        }
        if (!isget)
        {
            targetPuzzle = null;
        }
    }

    private List<RaycastResult> GraphicRaycaster(Vector2 pos)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        var mPointEventDate = new PointerEventData(UIEventSystem);
        mPointEventDate.position = pos;
        mPointEventDate.pressPosition = pos;
        UIGraphicRaycaster.Raycast(mPointEventDate, results);
        Debug.Log(results.Count);
        return results;
    }
}
