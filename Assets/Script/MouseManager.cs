using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour {

    public int Allhave=0;
    public Camera PlayerCam;
    private float CamRotation = 0f;
    private RaycastHit _hit;
    [SerializeField]
    private float RayLength = 3f;
    private string layerName1 = "Mutual";
    private string layerName2 = "Collection";
    private string layerName3 = "PuzzleBlock";
    private string layerName4 = "SteelSafe";
    private string layerName5 = "Case";
    private string layerName6 = "Computer";
    [SerializeField]
    private GameObject UIComputer;
    public GameObject hitGameObject;
    public bool MouseStaus = false;
    [SerializeField]
    private GameObject DoorManager;

    [SerializeField]
    private GameObject UIPuzzlePanle;
    [SerializeField]
    private GameObject UISteelSafe;

    [System.Serializable]
    struct Collections
    {
        public GameObject item;
        public bool have;
        public Text changeText;
        public GameObject Remove;
    }
    [SerializeField]
    List<Collections> collections=new List<Collections>();

    [SerializeField]
    private GameObject information;
    private bool DisplayText=false;

    [SerializeField]
    Behaviour[] undo;

    [SerializeField]
    private GameObject[] uiundo;


    private void Start()
    {
        ClearSelection(); 
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Cursor.lockState = CursorLockMode.Locked;
            able();
            DisplayText = false;
            foreach(GameObject a in uiundo)
            {
                if (a.activeSelf == true)
                {
                    a.SetActive(false);
                    break;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            MouseStaus = true;
        }else MouseStaus = false;

        if (DisplayText && MouseStaus)
        {
            DisplayText = false;
            information.SetActive(false);
            able();
        }

        if (!DisplayText)
        {
            float _XRot = Input.GetAxisRaw("Mouse Y");
            if (Mathf.Abs(_XRot) > float.Epsilon)
            {
                CamRotation += _XRot*1.5f;
                CamRotation = Mathf.Clamp(CamRotation, -85f, 85f);
                PlayerCam.transform.localEulerAngles = new Vector3(-CamRotation, 0, 0);
            }
        }
       
        //Debug.DrawRay(PlayerCam.transform.position, PlayerCam.transform.forward, Color.red, RayLength);
        if (Physics.Raycast(PlayerCam.transform.position, PlayerCam.transform.forward, out _hit, RayLength))
        {
            if (_hit.collider.gameObject.layer == LayerMask.NameToLayer(layerName1))
            {
                FindItsParent(_hit.collider.gameObject);
                HighLight(hitGameObject);
                // Debug.Log(hitGameObject.name);
                if (hitGameObject.GetComponent<Text>() != null&&MouseStaus&&!DisplayText)
                {
                    DisplayText = true;
                    Cursor.lockState = CursorLockMode.None;
                    StartCoroutine(TextDisplay());
                    unable();
                }
            }else if(_hit.collider.gameObject.layer == LayerMask.NameToLayer(layerName2))
            {
                FindItsParent(_hit.collider.gameObject);
                HighLight(hitGameObject);
                if (hitGameObject.GetComponent<Text>() != null&&MouseStaus&&!DisplayText)
                {
                    DisplayText = true;
                    Cursor.lockState = CursorLockMode.None;
                    StartCoroutine(TextDisplay());
                    unable();
                }
                if (MouseStaus)
                {
                    DoorManager.SendMessage("GetKey", hitGameObject);
                    CollectItem(hitGameObject);
                    Destroy(hitGameObject);
                }
            }else if(_hit.collider.gameObject.layer == LayerMask.NameToLayer(layerName3))
            {
                FindItsParent(_hit.collider.gameObject);
                HighLight(hitGameObject);
                if (collections[2].have&&MouseStaus)
                {
                    DisplayText = true;
                    Cursor.lockState = CursorLockMode.None;
                    UIPuzzlePanle.SetActive(true);
                    unable();
                }
            }else if(_hit.collider.gameObject.layer == LayerMask.NameToLayer(layerName4))
            {
                FindItsParent(_hit.collider.gameObject);
                HighLight(hitGameObject);
                if (MouseStaus)
                {
                    DisplayText = true;
                    Cursor.lockState = CursorLockMode.None;
                    UISteelSafe.SetActive(true);
                    unable();
                }
            }else if(_hit.collider.gameObject.layer == LayerMask.NameToLayer(layerName5))
            {
                FindItsParent(_hit.collider.gameObject);
                HighLight(hitGameObject);
                if (collections[5].have && MouseStaus)
                {
                    CollectItem(collections[6].item);
                    Destroy(collections[6].item);
                }
            }else if(_hit.collider.gameObject.layer == LayerMask.NameToLayer(layerName6))
            {
                FindItsParent(_hit.collider.gameObject);
                HighLight(hitGameObject);
                if (MouseStaus)
                {
                    unable();
                    DisplayText = true;
                    Cursor.lockState = CursorLockMode.None;
                    UIComputer.SetActive(true);
                }
                
            }
            else ClearSelection();
        }
        else ClearSelection();
    }

    void CollectItem(GameObject _hitGameObject)
    {
        for (int i = 0; i < collections.Count; i++)
        {
            if (collections[i].item == _hitGameObject)
            {
                if (collections[i].Remove != null)
                {
                    Destroy(collections[i].Remove.GetComponent<Text>());
                }
                collections[i].changeText.transform.parent.GetComponent<Button>().interactable = true;
                collections[i].changeText.text += collections[i].changeText.transform.GetChild(0).GetComponent<Text>().text;
                Collections temp = collections[i];
                temp.have = true;
                Allhave++;
                collections[i] = temp;
                break;
            }
        }
    }

    void HighLight(GameObject _hit)
    {
        if (_hit == null) return;
        if (_hit.GetComponent<cakeslice.Outline>() != null)
            {
                _hit.GetComponent<cakeslice.Outline>().changeColor(1);
            }
        foreach (Transform a in _hit.transform)
        {
            
            HighLight(a.gameObject);
        }
    }

    void CancelHighLight(GameObject _hit)
    {
        if (_hit == null) return;
        if (_hit.GetComponent<cakeslice.Outline>() != null)
            {
                _hit.GetComponent<cakeslice.Outline>().changeColor(0);
            }
        foreach (Transform a in _hit.transform)
        {
            CancelHighLight(a.gameObject);
        }
    }

    public int GetNum()
    {
        return Allhave;
    }

    void ClearSelection()
    {
        CancelHighLight(hitGameObject);
        hitGameObject = null;
    }

    private void FindItsParent(GameObject Its)
    {
        hitGameObject = Its;
        if (Its.transform.parent.gameObject.layer == Its.layer)
        {
            FindItsParent(Its.transform.parent.gameObject);
        }
        else
        {
            if (Its.transform.parent.gameObject.layer == LayerMask.NameToLayer("Door")&&MouseStaus)
            {
               DoorManager.SendMessage("CheckDoor",hitGameObject.transform.parent.gameObject);
            }
        }
    }
    private IEnumerator TextDisplay()
    {
        information.SetActive(true);
        string informations=hitGameObject.GetComponent<Text>().text;
        Text aim = information.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        aim.text = null;
        foreach(char a in informations)
        {
            aim.text += a;
            yield return new WaitForSeconds(0.1f);
        }

    }
    void unable()
    {
        foreach(Behaviour a in undo)
        {
            a.enabled = false;
        }
    }
    void able()
    {
        foreach(Behaviour a in undo)
        {
            a.enabled = true;
        }
    }
}
