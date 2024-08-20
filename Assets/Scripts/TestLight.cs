using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TestLight : MonoBehaviour
{   
    void Update()
    {
        GetComponent<Light2D>().intensity = Mathf.PingPong(Time.time, 1f);
    }
}
