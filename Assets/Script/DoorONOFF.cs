using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorONOFF : MonoBehaviour {

    [SerializeField]
    private const int Doornum=10;
    [System.Serializable]
    struct Door{
        public bool Staus;
        public GameObject DoorTran;
        public bool isopened;
        public GameObject key;
        public void ChangeStaus(bool value)
        {
            this.Staus = value;
        }
      //  public float 
    }
    Quaternion targetRotation;
    [SerializeField]
    List<Door> DoorArray = new List<Door>();

    [SerializeField]
    private float RotateSpeed = 5f;

    private bool OpenDoored = false;
    private bool CloseDoored = false;
    private GameObject TargetGameObject = null;

    public void CheckDoor(GameObject target)
    {
        for (int i = 0; i < DoorArray.Count; i++)
        {
            if (DoorArray[i].DoorTran == target)
            {
                if (!DoorArray[i].Staus&&DoorArray[i].isopened)
                {
                    Door temporeryDoor = DoorArray[i];
                    temporeryDoor.Staus = true;
                    DoorArray[i] = temporeryDoor;
                    OpenDoored = true;
                    targetRotation = Quaternion.Euler(temporeryDoor.DoorTran.transform.rotation.x, temporeryDoor.DoorTran.transform.rotation.y, temporeryDoor.DoorTran.transform.rotation.z - 90) * Quaternion.identity;
                    TargetGameObject = target;
                    
                }
                else
                {
                    CloseDoored = true;
                    Door temporeryDoor = DoorArray[i];
                    targetRotation = Quaternion.Euler(temporeryDoor.DoorTran.transform.rotation.x, temporeryDoor.DoorTran.transform.rotation.y, temporeryDoor.DoorTran.transform.rotation.z + 90) * Quaternion.identity;
                    temporeryDoor.Staus = false;
                    DoorArray[i] = temporeryDoor;
                    TargetGameObject = target;
                }
            }
        }
        /*
        foreach(Door go in DoorArray)
        {
            Debug.Log(1);
            if (go.DoorTran == target)
            {
                
                if (go.Staus)
                {
                    OpenDoored = false;
                    TargetGameObject = target;
                    DoorArray.Find(go)
                }
                else
                {
                    OpenDoored = true;
                    TargetGameObject = target;
                    go.ChangeStaus(true);
                }
            }
        }*/
    }
    private void Update()
    {
        if (OpenDoored)
        {
            OpenDoor(TargetGameObject);
        }
        if (CloseDoored)
        {
           CloseDoor(TargetGameObject);
        }
        
    }
    public void OpenDoor(GameObject door)
    {
        /*
        Vector3 now = door.transform.eulerAngles;
        Debug.Log(now.y);
        if (now.y < 180f)
        {
            OpenDoored = false;
            return;
        }
        door.GetComponent<Transform>().Rotate(new Vector3(0, -RotateSpeed*Time.deltaTime, 0),Space.World);
        */
        door.transform.rotation = Quaternion.Slerp(door.transform.localRotation,targetRotation,Time.deltaTime*0.1f);
    }
    public void CloseDoor(GameObject door)
    {
        /*
        Vector3 now = door.transform.eulerAngles;
        if (now.y > 270f)
        {
            CloseDoored = false;
            return;
        }
        door.GetComponent<Transform>().Rotate(new Vector3(0, RotateSpeed * Time.deltaTime, 0), Space.World);
        */
        Debug.Log(targetRotation.eulerAngles);
        door.transform.rotation = Quaternion.Slerp(door.transform.localRotation, targetRotation,Time.deltaTime*0.1f);
    }


    public void GetKey(GameObject _key)
    {
        for (int i = 0; i < DoorArray.Count; i++)
        {
            if (DoorArray[i].key == null) continue;
            if (DoorArray[i].key == _key)
            {
                Door temp = DoorArray[i];
                temp.isopened = true;
                DoorArray[i] = temp;
                break;
            }
        }
    }
}
