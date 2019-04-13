using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIPassword : MonoBehaviour {

    private InputField inputField;
    private string password = "631231";
    [SerializeField]
    private GameObject opinion;
    [SerializeField]
    private GameObject UILast;
    [SerializeField]
    private Text Display;
    public Scrollbar target;
    private void Start()
    {
        inputField = GetComponent<InputField>();
    }


    public void OnEndEdit()
    {
        if (inputField.text != password)
        {
            Image Panel = this.transform.parent.gameObject.GetComponent<Image>();
            Panel.color = Color.yellow;
            opinion.SetActive(true);
        }
        else
        {
            this.transform.parent.SetAsFirstSibling();
        }
    }

    public void ReadFile()
    {
        GameObject currentButton =EventSystem.current.currentSelectedGameObject;
        string TextLast = currentButton.transform.GetChild(1).GetComponent<Text>().text;
        UILast.SetActive(false);
        Display.text=TextLast;
    }

    public void ReturnField()
    {
        UILast.SetActive(true);
        target.value = 1f;
    }
}
