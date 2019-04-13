using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamMoving : MonoBehaviour {

    [SerializeField]
    private Camera Cam;
    Sequence mySequence;
    Sequence mysequence1;


    private void Start()
    {
        mySequence = DOTween.Sequence();
        mySequence.Append(Cam.transform.DOLocalMove(Cam.transform.localPosition - Vector3.up * 0.3f, 0.3f));
        mySequence.Append(Cam.transform.DOLocalMove(Cam.transform.localPosition, 0.3f));
        mySequence.Append(Cam.transform.DOLocalMove(Cam.transform.localPosition - Vector3.up * 0.3f, 0.3f));
        mySequence.Append(Cam.transform.DOLocalMove(Cam.transform.localPosition, 0.3f));
        mySequence.SetAutoKill(false);
        mySequence.SetLoops(-1);
        mySequence.Pause();
        mysequence1 = DOTween.Sequence();
        mysequence1.Append(Cam.transform.DOLocalMove(Cam.transform.localPosition - Vector3.up * 0.2f, 0.5f));
        mysequence1.Append(Cam.transform.DOLocalMove(Cam.transform.localPosition, 0.5f));
        mysequence1.Append(Cam.transform.DOLocalMove(Cam.transform.localPosition - Vector3.up * 0.2f, 0.5f));
        mysequence1.Append(Cam.transform.DOLocalMove(Cam.transform.localPosition, 0.5f));
        mysequence1.SetAutoKill(false);
        mysequence1.SetLoops(-1);
        mysequence1.Pause();
    }

    public void startWalking()
    {
        mysequence1.Play();
    }

    public void stopwalk()
    {
        mysequence1.Pause();
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
