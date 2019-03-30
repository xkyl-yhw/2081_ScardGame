using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamMoving : MonoBehaviour {

    [SerializeField]
    private Camera Cam;
    Sequence mySequence;

    private void Start()
    {
        mySequence = DOTween.Sequence();
        mySequence.Append(Cam.transform.DOLocalMove(Cam.transform.localPosition + Vector3.right * 0.3f - Vector3.up * 0.3f, 0.3f));
        mySequence.Append(Cam.transform.DOLocalMove(Cam.transform.localPosition, 0.3f));
        mySequence.Append(Cam.transform.DOLocalMove(Cam.transform.localPosition - Vector3.right * 0.3f - Vector3.up * 0.3f, 0.3f));
        mySequence.Append(Cam.transform.DOLocalMove(Cam.transform.localPosition, 0.3f));
        mySequence.SetAutoKill(false);
        mySequence.SetLoops(-1);
        mySequence.Pause();
    }

    public void Stop()
    {
        mySequence.Pause();
    }

    public void Play()
    {
        mySequence.Play();
    }
}
