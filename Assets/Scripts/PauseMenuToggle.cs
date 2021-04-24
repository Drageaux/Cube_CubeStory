using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuToggle : MonoBehaviour
{
    private CanvasGroup canvasGroup; 

    private void Awake()
    {
        if (GetComponent<CanvasGroup>()!=null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        else
        {
            Debug.LogError("component is null");
        }
        }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (canvasGroup.interactable)
            {
                Time.timeScale = 1f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
                Cursor.visible = false;
            }
            else
            {
                Time.timeScale = 0f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
                Cursor.visible = true;
            }
        }

    }
}
