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
        public IEnumerator<GameObject> GetEnumerator()
        {
            return GetGameObject();
        }
        public IEnumerator<GameObject> GetGameObject()
        {
            yield return DoorTran;
        }
    }
    [SerializeField]
    Door[] DoorArray = new Door[Doornum];
    public void CheckDoor(GameObject target)
    {
        foreach(GameObject go in DoorArray.GetEnumerator())
        {

        }
    }
    public void OpenDoor()
    {

    }
    public void CloseDoor()
    {

    }
}
