using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HardCode : MonoBehaviour
{
    public int count = 0;
    public GameObject aple;
    public GameObject apl2;

    public GameObject text;

    public GameObject pnl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {

            //to pick apple
            //dim apple
            aple.SetActive(false);
                //add apple text
                text.GetComponent<Text>().text = "apple";
        }
        if (Input.GetKey(KeyCode.O))
        {
            
                //to put apple into pan
                //dim apple text
                text.GetComponent<Text>().text = "";
            //add apple in pan
            apl2.SetActive(true);
               
            
        }
        if (Input.GetKey(KeyCode.I))
        {

            //to get baked apple
            //dim apple in pan
            apl2.SetActive(false);
            //add baked apple text
            text.GetComponent<Text>().text = "baked\napple";
                
           
        }
        if (Input.GetKey(KeyCode.U))
        {
           
                //to deliver baked apple
                //dim baked apple text
                text.GetComponent<Text>().text = "";
            //dim apple panel
            pnl.SetActive(false);
           
        }
    }
}
