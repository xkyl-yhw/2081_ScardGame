using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End1 : MonoBehaviour {

    [SerializeField]
    private GameObject EndUI;
    public MouseManager mm;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (mm.GetNum() > 9)
            {
                EndUI.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text="Achieve All Collection";
            }
            EndUI.SetActive(true);
        }
    }
}
