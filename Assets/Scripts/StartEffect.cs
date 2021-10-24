using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using EZCameraShake;

public class StartEffect : MonoBehaviour
{
    float lowerLimit = 0;
    bool coroutine = true;
    bool shaken = false;
    void Start()
    {

/*         v = gameObject.GetComponent<Volume>();
        v.profile.TryGet(out ca);
        v.profile.TryGet(out pp);
        pp.distance.Override(1f);
        ca.intensity.Override(1f); */

    }
    public void DoShake(float strength){
            CameraShaker.Instance.ShakeOnce(2f, 4f, .1f, 0.8f);

    }


}