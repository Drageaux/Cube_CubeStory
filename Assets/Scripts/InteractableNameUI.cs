using UnityEngine;
using UnityEngine.UI;

public class InteractableNameUI : MonoBehaviour
{
    private Interactable focusedTarget;
    private Text textObj;

    InteractionManager interactionManager;

    private void Start()
    {
        interactionManager = InteractionManager.instance;
        interactionManager.onInteractableFocused += DisplayUI;
        interactionManager.onInteractableDefocused += HideUI;
        textObj = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }

    public void DisplayUI(Interactable i)
    {
        gameObject.SetActive(true);
        focusedTarget = i;
        textObj.text = i.name;
    }

    public void HideUI(Interactable i)
    {
        if (focusedTarget == i)
        {
            gameObject.SetActive(false);
            focusedTarget = null;
            textObj.text = "";
        }
    }
}
