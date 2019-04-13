using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Openning : MonoBehaviour {

    public GameObject rightDoor;
    public GameObject LeftDoor;
    public GameObject PlayerCam;
    private float shakeLength = 3f;
    Sequence mySquence;
    private Vector3 originVector;
    private float shakeAmout = 0.1f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        originVector = PlayerCam.transform.localPosition;
        mySquence = DOTween.Sequence();
        mySquence.Append(rightDoor.transform.DOLocalMove(rightDoor.transform.localPosition + Vector3.forward,3f));
        mySquence.Insert(0f, LeftDoor.transform.DOLocalMove(LeftDoor.transform.localPosition - Vector3.forward, 3f));
        mySquence.SetAutoKill(false);
        mySquence.Pause();
    }

    private void Update()
    {
        Cursor.visible = true;
        
        if (shakeLength > 0f)
        {
            PlayerCam.transform.localPosition = originVector + Random.insideUnitSphere * shakeAmout;
            shakeLength -= Time.deltaTime;
        }
        else
        {
            StartCoroutine(opendoor());
            shakeLength = 0f;
            PlayerCam.transform.localPosition = originVector;
        }
        
    }

    private IEnumerator opendoor()
    {
        yield return new WaitForSeconds(1f);
        mySquence.PlayForward();
    }



}
