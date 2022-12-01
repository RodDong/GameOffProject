using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class streetAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource streetAudio1, streetAudio2, streetAudio3;
    float timer1 = 0.0f;
    float timer2 = 0.0f;
    float TOTAL_TIME = 212.0f;
    void Start()
    {
        streetAudio1 = GetComponent<AudioSource>();
        streetAudio1.time = 0.0f;
        streetAudio1.Play();
        streetAudio3.Play();
    }

    void Update()
    {
        playOST();
    }

    void playOST()
    {
        if (streetAudio1.isPlaying)
        {
            timer1 += Time.deltaTime;
        }
        if (streetAudio2.isPlaying)
        {
            timer2 += Time.deltaTime;
        }


        if (timer1 >= TOTAL_TIME)
        {
            streetAudio1.Stop();
            timer1 = 0.0f;
        }
        if (timer2 >= TOTAL_TIME)
        {
            streetAudio2.Stop();
            timer2 = 0.0f;
        }

        if (timer1 + 4.0f >= TOTAL_TIME && !streetAudio2.isPlaying)
        {
            streetAudio2.Play();
        }

        if (timer2 + 4.0f >= TOTAL_TIME && streetAudio1.isPlaying)
        {
            streetAudio1.Play();
        }
    }
}
