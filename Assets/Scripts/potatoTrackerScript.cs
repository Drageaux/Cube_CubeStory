using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class potatoTrackerScript : MonoBehaviour
{
    private Image iconIg;
    private Text distanceText;

    public Transform player;
    public Transform target;
    public Camera cam;
    public float closeEnough;
    // Start is called before the first frame update
    void Start()
    {
        iconIg = GetComponent<Image>();
        distanceText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null){
            GetDistance();
            // CheckonScreen();
        }
    }

    private void GetDistance(){
        float dist = Vector3.Distance(player.position, target.position);
        distanceText.text = dist.ToString("f1") + "m"; 

        float thing = Vector3.Dot((target.position - cam.transform.position).normalized, cam.transform.forward);
        if (thing <= 0 ||  dist < closeEnough){
            distanceText.text = "Potato Field";
            // transform.position = cam.WorldToScreenPoint(target.position);
            if (Input.GetKeyDown(KeyCode.E)){
                Destroy(gameObject);
            }
            

        } else {
            ToggleUi(true);
            Vector3 pos = target.position;
            pos.y += 2;
            iconIg.transform.position = cam.WorldToScreenPoint(pos);
            // iconIg.transform.position.Set(target.position.x, target.position.y, target.position.z);
        }

        // if(dist < closeEnough){
        //     ToggleUi(false);
        // }
    }

    // private void CheckonScreen(){
    //     float thing = Vector3.Dot((target.position - cam.transform.position).normalized, cam.transform.forward);
    //     if (thing <= 0 ){
    //         ToggleUi(false);
    //         // transform.position = cam.WorldToScreenPoint(target.position);
    //     } else {
    //         ToggleUi(true);
    //         transform.position = cam.WorldToScreenPoint(target.position);
    //     }
    // }

    private void ToggleUi(bool value){
        iconIg.enabled = value;
        distanceText.enabled = value;
    }
}
