using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IOS 
using UnityEngine.iOS;
#endif

public class KeepApect : MonoBehaviour
{
    private Camera bgCam;
    private Camera mainCam;

    void Awake()
    {
        bgCam = GetComponent<Camera>();
        mainCam = Camera.main;

        float nineBy16 = 9f / 16f;

        if (bgCam.aspect < nineBy16)
        {
            mainCam.rect = new Rect(0f, (1.0f - bgCam.aspect / nineBy16) / 2.0f, 1.0f, bgCam.aspect / nineBy16);

        }
        else if (bgCam.aspect > nineBy16)
        {
            mainCam.rect = new Rect((1.0f - nineBy16 / bgCam.aspect) / 2.0f, 0, nineBy16 / bgCam.aspect, 1.0f);
        }
    }
}