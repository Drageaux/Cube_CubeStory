using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popUp : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    // Start is called before the first frame update
    public void PopUp()
    {
        
        popUpBox.SetActive(true);
        animator.SetTrigger("pop");
      

    }
    public void close()
    {
       
        animator.SetTrigger("close");
    }
}
