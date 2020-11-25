using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartObjectDestroy()
    {
        Destroy(gameObject, 2.0f);
    }
}
