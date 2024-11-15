using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : Interactable
{
    [SerializeField]
    private GameObject player;
    private bool sitting;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        sitting = !sitting;
        Debug.Log("interacting with bench :3" + sitting);
        //change input mode
        if(sitting){
            player.GetComponent<InputManager>().SwitchToSitting(this);
        }else{
            player.GetComponent<InputManager>().SwitchToStanding();
        }

        //enable animations
        
    }
}
