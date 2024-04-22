using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volume : MonoBehaviour
{
    private AudioSource AudioSrc;
    private float musicVolume = 1f;
    public GameObject volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        AudioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        AudioSrc.volume = volumeSlider.GetComponent<Slider>().value;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
