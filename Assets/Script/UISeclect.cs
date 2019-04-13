using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISeclect : MonoBehaviour {

    private MouseManager mm;

    private void Start()
    {
        mm = GameObject.Find("Player").gameObject.GetComponent<MouseManager>();
    }

    private void Update()
    {
        if (mm.hitGameObject != null)
        {
            Debug.Log(1);
            if (mm.hitGameObject.layer == LayerMask.NameToLayer("Mutual"))
                this.transform.GetChild(0).gameObject.SetActive(true);
            else this.transform.GetChild(1).gameObject.SetActive(true);

            Bounds bigBound = mm.hitGameObject.GetComponent<Renderer>().bounds;
            Vector3 ScreenPoint = Camera.main.WorldToScreenPoint(bigBound.center);
            RectTransform imageTran = this.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
            imageTran.position = new Vector2(ScreenPoint.x, ScreenPoint.y);
        }else 
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
