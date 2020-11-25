using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cam : MonoBehaviour
{
    public Transform cam;
    public static Cam camera;
    //// Start is called before the first frame update
    void Start()
    {
        camera = this;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
    public void ShakeCamera()
    {
        cam.DOShakePosition(0.25f);
    }
}
