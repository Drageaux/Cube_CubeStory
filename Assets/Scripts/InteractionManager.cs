using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    #region Singleton

    public static InteractionManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Interaction Manager found!");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnInteractableFocused(Interactable i);
    public OnInteractableFocused onInteractableFocused;
    public delegate void OnInteractableDefocused(Interactable i);
    public OnInteractableDefocused onInteractableDefocused;
    public Interactable CurrentTarget
    {
        get;
        private set;
    }

    public void FocusNewTarget(Interactable i)
    {
        CurrentTarget = i;
        onInteractableFocused.Invoke(i);
    }

    public void DefocusCurrentTarget(Interactable i)
    {
        print(CurrentTarget);
        print(i);
        if (CurrentTarget == i)
        {
            CurrentTarget = null;
            onInteractableDefocused.Invoke(i);
        }
    }
}
