using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndooAudioManager2 : MonoBehaviour
{
    [SerializeField] AudioSource indoorAudio1, indoorAudio2, indoorAudio3;
    float timer1 = 0.0f;
    float timer2 = 0.0f;
    float TOTAL_TIME = 109.0f;
    void Start()
    {
        indoorAudio1 = GetComponent<AudioSource>();
        indoorAudio1.time = 0.0f;
        indoorAudio1.Play();
        indoorAudio3.Play();
    }

    void Update()
    {
        playOST();
    }

    void playOST()
    {
        if (indoorAudio1.isPlaying)
        {
            timer1 += Time.deltaTime;
        }
        if (indoorAudio2.isPlaying)
        {
            timer2 += Time.deltaTime;
        }


        if (timer1 >= TOTAL_TIME)
        {
            indoorAudio1.Stop();
            timer1 = 0.0f;
        }
        if (timer2 >= TOTAL_TIME)
        {
            indoorAudio2.Stop();
            timer2 = 0.0f;
        }

        if (timer1 + 10.0f >= TOTAL_TIME && !indoorAudio2.isPlaying)
        {
            Debug.Log(1);
            indoorAudio2.Play();
        }

        if (timer2 + 10.0f >= TOTAL_TIME && indoorAudio1.isPlaying)
        {
            indoorAudio1.Play();
        }
    }
}
