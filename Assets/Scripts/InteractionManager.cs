using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    #region Singleton

    public static InteractionManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public delegate void OnInteractableFocused(Interactable i);
    public OnInteractableFocused onInteractableFocused;
    public delegate void OnInteractableDefocused(Interactable i);
    public OnInteractableDefocused onInteractableDefocused;
}
