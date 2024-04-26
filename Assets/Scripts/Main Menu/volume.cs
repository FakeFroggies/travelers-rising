using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameVolume : MonoBehaviour
{
    private AudioSource AudioSrc;
    public GameObject volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        AudioSrc = GetComponent<AudioSource>();
        AudioSrc.volume = PlayerPrefs.GetFloat("music", 0.3f);
        volumeSlider.GetComponent<Slider>().value = AudioSrc.volume;
    }

    // Update is called once per frame
    void Update()
    {
        AudioSrc.volume = volumeSlider.GetComponent<Slider>().value;
    }

    public void SaveSound()
    {
        PlayerPrefs.SetFloat("music", AudioSrc.volume);
    }
}
