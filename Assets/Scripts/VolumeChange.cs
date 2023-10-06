using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour
{
    public AudioSource audioPlayer;
    public Slider audioSlider;

    private void Start()
    {

        audioPlayer.volume = PlayerPrefs.GetFloat("audioVolume", 0.5f);
        audioSlider.value = audioPlayer.volume;

        audioSlider.onValueChanged.AddListener((value) =>
        {

            audioPlayer.volume = value;
            PlayerPrefs.SetFloat("audioVolume", value);
        });

    }
}
