using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOpener : MonoBehaviour
{
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    void Update()
    {
         if (Input.GetKeyDown(KeyCode.M)){
             if (panel.activeSelf== false)
            {
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }
         }
    }
    public void OpenPanel(){
         if (panel.activeSelf== false)
            {
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }
    }

}