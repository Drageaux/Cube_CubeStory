using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource[] levelMusic;
    public AudioSource endLevelMusic;
    public AudioSource mainMenuMusic;

    private int currLevel;
    // Start is called before the first frame update
    void Start()
    {
        this.currLevel = 0;
        this.StartLevelMusic();
    }

    public void PlayNextLevelMusic()
    {
        this.StopMusic(this.levelMusic[this.currLevel]);
        this.currLevel++;
        this.PlayMusic(this.levelMusic[this.currLevel]);
    }

    public void PlayEndLevelMusic()
    {
        this.StopMusic(this.levelMusic[this.currLevel]);
        this.PlayMusic(this.endLevelMusic);
    }

    void StopMusic(AudioSource currMusic)
    {
        currMusic.Stop();
    }

    void PlayMusic(AudioSource currMusic)
    {
        currMusic.Play();
    }

    void StartLevelMusic()
    {
        this.PlayMusic(this.levelMusic[this.currLevel]);
    }

}
