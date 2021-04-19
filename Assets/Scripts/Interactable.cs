using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    // reference: https://www.youtube.com/watch?v=9tePzyL6dgc&list=PLPV2KyIb3jR4KLGCCAciWQ5qHudKtYeP7&index=3
    public float radius = 3f;
    new public string name = "Interactable";
    protected InteractableType type;
    bool isFocus = false;
    bool hasInteracted = false;

    protected InteractionManager interactionManager;

    public virtual void Interact()
    {
        // This method is meant to be overwritten
        Debug.Log("Interacting " + transform.name);
    }

    private void Start()
    {
        interactionManager = InteractionManager.instance;
    }

    private void Update()
    {
        if (isFocus)
        {
            // TODO: change color
        }
    }

    public void OnInteracted()
    {
        this.hasInteracted = true;
        interactionManager.onInteractableDefocused.Invoke(this);
    }

    public void OnFocused()
    {
        isFocus = true;
        interactionManager.onInteractableFocused.Invoke(this);
    }

    public void OnDefocused()
    {
        isFocus = false;
        interactionManager.onInteractableDefocused.Invoke(this);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

public enum InteractableType
{
    Tool,
    Ingredient,

}