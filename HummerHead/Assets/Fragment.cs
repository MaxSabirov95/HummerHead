using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    public void StartObjectDestroy()
    {
        Destroy(gameObject, 2.0f);
    }
}
