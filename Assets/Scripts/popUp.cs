using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popUp : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = popUpBox.GetComponent<CanvasGroup>();
        if (canvasGroup == null) {
            Debug.LogError("component is null");
        }
        close();
    }

    // Start is called before the first frame update
    public void PopUp()
    {
        Time.timeScale = 0f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        Cursor.visible = true;
        animator.SetTrigger("pop");
    }
    public void close()
    {
        Time.timeScale = 1f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
        //Cursor.visible = false;
        animator.SetTrigger("close");
    }
}
