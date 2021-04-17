using UnityEngine;
using UnityEngine.UI;

public class InteractableNameUI : MonoBehaviour
{
    private string str = "";
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

    public void DisplayUI(string name)
    {
        gameObject.SetActive(true);
        str = name;
        textObj.text = name;
    }

    public void HideUI(string name)
    {
        if (str == name)
        {
            gameObject.SetActive(false);
            str = "";
            textObj.text = "";
        }
    }
}
