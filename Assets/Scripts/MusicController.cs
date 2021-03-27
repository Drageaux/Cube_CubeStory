using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public GameObject musicObject;
    private Music music;
    // Start is called before the first frame update
    void Start()
    {
        this.music = this.musicObject.GetComponent<Music>();
    }

    public void TriggerEndLevelMusic()
    {
        this.music.PlayEndLevelMusic();
    }

    public void TriggerNextLevelMusic()
    {
        this.music.PlayNextLevelMusic();
    }
}
