using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    // reference: https://www.youtube.com/watch?v=9tePzyL6dgc&list=PLPV2KyIb3jR4KLGCCAciWQ5qHudKtYeP7&index=3
    public float radius = 3f;
    new public string name = "Interactable";
    public InteractableType type;
    protected bool isFocus = false;
    protected bool hasInteracted = false;

    protected InteractionManager interactionManager;

    public virtual void Interact()
    {
        // This method is meant to be overwritten
        Debug.Log("Interacting " + transform.name);
    }

    protected virtual void Start()
    {
        interactionManager = InteractionManager.instance;
    }

    public void OnFocused()
    {
        isFocus = true;
        interactionManager.FocusNewTarget(this);
    }

    public void OnDefocused()
    {
        isFocus = false;
        interactionManager.DefocusCurrentTarget(this);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.OnFocused();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.OnDefocused();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactionManager.CurrentTarget == null)
            {
                this.OnFocused();
            } 
        }
    }
}

public enum InteractableType
{
    Tool,
    Ingredient,
}