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

    public delegate void OnInteractableFocused(string name);
    public OnInteractableFocused onInteractableFocused;
    public delegate void OnInteractableDefocused(string name);
    public OnInteractableDefocused onInteractableDefocused;
}
