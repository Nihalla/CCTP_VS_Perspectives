using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Transfer : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] audio_sources = GameObject.FindGameObjectsWithTag("Audio");
        if(audio_sources.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
