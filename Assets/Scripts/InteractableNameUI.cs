using UnityEngine;
using UnityEngine.UI;

public class InteractableNameUI : MonoBehaviour
{
    private string str = "";
    private Text textObj;
    public CanvasGroup ui;


    private void Start()
    {
        this.textObj = GetComponentInChildren<Text>();
    }

    void OnInteractableFocused(string name)
    {
        str = name;
        textObj.text = name;
        ui.alpha = 1f;
    }

    void OnInteractableDefocused(string name)
    {
        if (str == name)
        {
            str = "";
            textObj.text = "";
            ui.alpha = 0f;
        }
    }
}
