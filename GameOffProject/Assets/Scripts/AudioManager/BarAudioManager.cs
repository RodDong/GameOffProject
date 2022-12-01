using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarAudioManager : MonoBehaviour
{
    [SerializeField]AudioSource barAudio1, barAudio2, barAudio3;
    float timer1 = 0.0f;
    float timer2 = 0.0f;
    float TOTAL_TIME = 120.0f;
    void Start()
    {
        barAudio1 = GetComponent<AudioSource>();
        barAudio1.time = 0.0f;
        barAudio1.Play();
        barAudio3.Play();
    }

    void Update()
    {
        playOST();
    }

    void playOST()
    {
        if (barAudio1.isPlaying)
        {
            Debug.Log("1 playing");
            timer1 += Time.deltaTime;
        }
        if (barAudio2.isPlaying)
        {
            Debug.Log("2 playing");
            timer2 += Time.deltaTime;
        }


        if (timer1 >= TOTAL_TIME)
        {
            Debug.Log(1);
            barAudio1.Stop();
            timer1 = 0.0f;
        }
        if (timer2 >= TOTAL_TIME)
        {
            Debug.Log(2);
            barAudio2.Stop();
            timer2 = 0.0f;
        }

        if (timer1 + 4.5f >= TOTAL_TIME && !barAudio2.isPlaying)
        {
            Debug.Log(3);
            barAudio2.Play();
        }

        if (timer2 + 4.5f >= TOTAL_TIME && barAudio1.isPlaying)
        {
            Debug.Log(4);
            barAudio1.Play();
        }
    }
}
