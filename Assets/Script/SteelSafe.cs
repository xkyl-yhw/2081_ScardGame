using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SteelSafe : MonoBehaviour {

    private string password="1802";
    [SerializeField]
    private Text[] _passwordCon;
    [SerializeField]
    private GameObject DoorSafe;
    private bool opened = false;
    private float RotateSpeed = 20f;

    private void Update()
    {
        string temp=null;
        foreach(Text a in _passwordCon)
        {
            temp += a.text;
        }
        if (temp == password)
        {
            if(!opened)
                OpenTheSteelSafe();
        }
        Debug.Log(temp);
    }

    public void OpenTheSteelSafe()
    {
        Quaternion now = DoorSafe.transform.localRotation;
        if (now.z<-0.9f)
        {
            opened = true;
            return;
        }
        DoorSafe.transform.Rotate(Vector3.forward, -RotateSpeed * Time.deltaTime);
    }

    public void IncreaseNum()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        for (int i = 0; i < button.transform.parent.gameObject.transform.childCount; i++)
        {
            if (button.transform.parent.gameObject.transform.GetChild(i).gameObject.GetComponent<Text>() != null)
            {
                string num = button.transform.parent.gameObject.transform.GetChild(i).gameObject.GetComponent<Text>().text;
                char nums = num[0];
                if (nums == '9')
                {
                    nums = '0';
                }
                else nums++;
                string num1 = nums.ToString();
                button.transform.parent.gameObject.transform.GetChild(i).gameObject.GetComponent<Text>().text = num1;
            }

        }
    }

    public void DecreaseNum()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        for (int i = 0; i < button.transform.parent.gameObject.transform.childCount; i++)
        {
            if (button.transform.parent.gameObject.transform.GetChild(i).gameObject.GetComponent<Text>() != null)
            {
                Text buttonText = button.transform.parent.gameObject.transform.GetChild(i).gameObject.GetComponent<Text>();
                string num =buttonText.text;
                char nums = num[0];
                if (nums == '0')
                {
                    nums = '9';
                }
                else nums--;
                string num1 = nums.ToString();
                buttonText.text = num1;
            }

        }
    }
}
