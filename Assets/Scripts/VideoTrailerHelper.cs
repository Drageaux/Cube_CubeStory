using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoTrailerHelper : MonoBehaviour
{
    public List<CanvasGroup> uiCanvases = new List<CanvasGroup>();
    public bool displayUI = true;
    // Start is called before the first frame update
    void Start()
    {
        if (displayUI)
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (CanvasGroup c in uiCanvases)
        {
            if (c)
                c.alpha = 0f;
        }
    }
}
