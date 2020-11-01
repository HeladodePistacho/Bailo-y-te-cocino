using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource music1;
    public AudioSource music2;
    public AudioSource music3;
    public AudioSource music4;

    public AudioSource beatAS;

    private static SoundManager soundInstance;

    public static SoundManager Instance
    {
        get
        {
            return soundInstance;
        }
    }

    private void Awake()
    {
        if (soundInstance == null)
            soundInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
