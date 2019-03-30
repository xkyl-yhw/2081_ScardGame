using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

    private Camera PlayerCam;
    private float CamRotation = 0f;
    private RaycastHit _hit;
    [SerializeField]
    private float RayLength = 3f;
    private string layerName1 = "Mutual";
    private string layerName2 = "Collection";
    public GameObject hitGameObject;

    private void Start()
    {
        ClearSelection();
        PlayerCam = Camera.main;    
    }

    private void Update()
    {
        float _XRot = Input.GetAxisRaw("Mouse Y");
        if (Mathf.Abs(_XRot) > float.Epsilon)
        {
            CamRotation += _XRot;
            CamRotation = Mathf.Clamp(CamRotation, -85f, 85f);
            PlayerCam.transform.localEulerAngles = new Vector3(-CamRotation, 0, 0);
        }
        Debug.DrawRay(PlayerCam.transform.position, PlayerCam.transform.forward, Color.red, RayLength);
        if (Physics.Raycast(PlayerCam.transform.position, PlayerCam.transform.forward, out _hit, RayLength))
        {
            if (_hit.collider.gameObject.layer == LayerMask.NameToLayer(layerName1) || _hit.collider.gameObject.layer == LayerMask.NameToLayer(layerName2))
            {
                hitGameObject = _hit.collider.gameObject;
            }
            else ClearSelection();
        }
        else ClearSelection();
    }
    void ClearSelection()
    {
        hitGameObject = null;
    }
}
