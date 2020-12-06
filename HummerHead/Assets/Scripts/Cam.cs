using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cam : MonoBehaviour
{
    public Transform cam;
    public static Cam camera;
    public float followSpeed;
    public float Y;
    //// Start is called before the first frame update
    void Start()
    {
        camera = this;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 CamPos = BlackBoard.player.transform.position;
        CamPos.z = -10;
        CamPos.y = Mathf.Max(CamPos.y,Y);
        transform.position = Vector3.Lerp(transform.position, CamPos, followSpeed * Time.deltaTime);
    }

    public void ShakeCamera()
    {
        cam.DOShakePosition(0.25f);
    }
}
