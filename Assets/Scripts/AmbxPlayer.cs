using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbxPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    [SerializeField] private AudioClip[] creaks, movement, water, wind;
    [SerializeField] private float minWaitTime, maxWaitTime;
    private float timer;


    private void Awake()
    {

    }

    private void Update()
    {
        if(timer <= 0 && !source.isPlaying)
        {
            PlayAudio(GetRandomClipInArray());
            timer = Random.Range(minWaitTime, maxWaitTime);
        }
        else if (!source.isPlaying)
        {
            timer -= Time.deltaTime;
        }
    }

    private AudioClip[] GetAudioList()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                return creaks;           
            case 1:
                return movement;
            case 2:
                return water;
            case 3:
                return wind;
        }
        return creaks;
    }

    private AudioClip GetRandomClipInArray()
    {
        AudioClip clip;

        AudioClip[] array = GetAudioList();

        clip = array[Random.Range(0, array.Length)];

        return clip;
    }

    private void PlayAudio(AudioClip audio)
    {
        source.clip = audio;
        source.Play();
    }
}
