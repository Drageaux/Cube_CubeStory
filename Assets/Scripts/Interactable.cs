using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    // reference: https://www.youtube.com/watch?v=9tePzyL6dgc&list=PLPV2KyIb3jR4KLGCCAciWQ5qHudKtYeP7&index=3
    new public string name;
    public float radius = 3f;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    public virtual void Interact()
    {
        // This method is meant to be overwritten
        Debug.Log("Interacting " + transform.name);
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
    }

    public void OnFocused()
    {
        isFocus = true;
        onInteractableFocusedCallback.Invoke(name);
    }

    public void OnDefocused()
    {
        isFocus = false;
        onInteractableDefocusedCallback.Invoke(name);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
