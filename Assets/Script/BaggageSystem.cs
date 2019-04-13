using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaggageSystem : MonoBehaviour {

    [SerializeField]
    private GameObject UIBaggage;
    private bool bagPerform = false;
    private static GameObject LastClick = null;
    [SerializeField]
    private GameObject targetPanel;

	void Update () {
        if (Input.GetKeyUp(KeyCode.B))
        {
            Cursor.lockState = CursorLockMode.None;
            if (!bagPerform)
            {
                Time.timeScale = 0f;
                bagPerform = true;
                UIBaggage.SetActive(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
                bagPerform = false;
                UIBaggage.SetActive(false);
            }
            
        }
	}

    public void OnClick()
    {
        var buttonSelf = EventSystem.current.currentSelectedGameObject;
        if (buttonSelf.GetComponent<Button>().interactable == true)
        {
            if (LastClick != null)
            {   
                LastClick.transform.GetChild(0).GetComponent<Text>().color = Color.grey;
             }
             LastClick = buttonSelf;
             LastClick.transform.GetChild(0).GetComponent<Text>().color = Color.white;
             targetPanel.GetComponent<Text>().text = LastClick.transform.GetChild(1).GetComponent<Text>().text;
        }
        
    }
}
