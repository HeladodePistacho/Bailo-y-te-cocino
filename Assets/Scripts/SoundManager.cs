using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource music1;
    public AudioSource music2;
    public AudioSource music3;
    public AudioSource music4;

    public AudioSource pickIngredient;
    public AudioSource failIngredient;
    public AudioSource recipeComplete;
    public AudioSource eat;

    public AudioSource beat;

    private static SoundManager soundInstance;

    public enum FX
    {
        PICK_INGREDIENT = 0,
        FAIL_INGREDIENT,
        RECIPE_COMPLETE,
        EAT
    }
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
        {
            soundInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        music1.volume = 1;
        music2.volume = 0;
        music3.volume = 0;
        music4.volume = 0;
        beat.volume = 0;
    }

    public void PlayFX(FX toPlay)
    {
        switch (toPlay)
        {
            case (FX.PICK_INGREDIENT):
                pickIngredient.Play();
                break;
            case (FX.FAIL_INGREDIENT):
                failIngredient.Play();
                break;
            case (FX.RECIPE_COMPLETE):
                recipeComplete.Play();
                break;
            case (FX.EAT):
                eat.Play();
                break;
        }
    }

    public void SetMusicStage(int stage)
    {
        switch(stage)
        {
            case 1:
                music2.volume = 0;
                music3.volume = 0;
                music4.volume = 0;
                break;
            case 2:
                music2.volume = 0.5f;
                music3.volume = 0;
                music4.volume = 0;
                break;
            case 3:
                music2.volume = 0;
                music3.volume = 0.5f;
                music4.volume = 0;
                break;
            case 4:
                music2.volume = 0;
                music3.volume = 0;
                music4.volume = 0.5f;
                break;
        }
    }
}
