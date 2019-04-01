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
        public void ChangeStaus(bool value)
        {
            this.Staus = value;
        }
    }
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
                if (DoorArray[i].Staus)
                {
                    CloseDoored = true;
                    Door temporeryDoor = DoorArray[i];
                    temporeryDoor.Staus = false;
                    DoorArray[i] = temporeryDoor;
                    TargetGameObject = target;
                }
                else
                {
                    Door temporeryDoor = DoorArray[i];
                    temporeryDoor.Staus = true;
                    DoorArray[i] = temporeryDoor;
                    OpenDoored = true;
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
        if (door.transform.localRotation.z > 0f)
        {
            OpenDoored = false;
            return;
        }
        door.GetComponent<Transform>().RotateAround(door.transform.position,door.transform.forward,Time.deltaTime*RotateSpeed);
    }
    public void CloseDoor(GameObject door)
    {
        if (door.transform.localRotation.z < -0.5f)
        {
            CloseDoored = false;
            return;
        }
        door.GetComponent<Transform>().RotateAround(door.transform.position, door.transform.forward, -Time.deltaTime * RotateSpeed);
    }
}
