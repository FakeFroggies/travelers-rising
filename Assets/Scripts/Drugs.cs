using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Drugs : MonoBehaviour
{
    UnityEngine.Rendering.VolumeProfile volumeProfile;
    public float hueSpeed = 0.5f;
    public bool onDrugs = false;

    //HUE
    float hueT, currentHue, prevHue, nextHue;
    //BLOOM
    float bloomT, bloomIntensity, prevBloom, nextBloom;
    //BLOOM COLORS
    Color prevC, nextC;

    private void Start()
    {
        volumeProfile = GetComponent<Volume>()?.sharedProfile;
        nextHue = UnityEngine.Random.Range(-180, 180);
        prevHue = 0;
        nextC = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        prevC = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        prevBloom = 50;
        nextBloom = UnityEngine.Random.Range(50, 500);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            onDrugs = !onDrugs;
        }
        if (onDrugs)
        {
            currentHue = Mathf.Lerp(prevHue, nextHue, hueT);
            bloomIntensity = Mathf.Lerp(prevBloom, nextBloom, bloomT);
            hueT += Time.deltaTime / 2f;
            bloomT += Time.deltaTime / 2f;
            if (currentHue == nextHue)
            {
                hueT = 0f;
                prevHue = nextHue;
                nextHue = UnityEngine.Random.Range(-180, 180);
            }
            if (bloomIntensity == nextBloom)
            {
                bloomT = 0;
                prevBloom = nextBloom;
                nextBloom = UnityEngine.Random.Range(50, 500);
            }
            if (volumeProfile.TryGet(out Bloom bloom))
            {
                bloom.intensity.Override(bloomIntensity);
                bloom.tint.Override(Color.Lerp(prevC, nextC, bloomT));
                if (bloom.tint.value == nextC)
                {
                    prevC = nextC;
                    nextC = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                }
            }
            if (volumeProfile.TryGet(out ColorAdjustments ca))
                ca.hueShift.Override(currentHue);
        }
        else
        {
            if (volumeProfile.TryGet(out Bloom bloom))
            {
                bloom.intensity.Override(0);
                bloom.tint.Override(new Color(0, 0, 0));
            }
            if (volumeProfile.TryGet(out ColorAdjustments ca))
                ca.hueShift.Override(0);
        }
    }
}
