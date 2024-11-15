using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScanner : Interactable
{
    [SerializeField]
    private GameObject slidingDoor;
    private bool open;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        open = !open;

        //enable animations
        slidingDoor.GetComponent<Animator>().SetBool("IsOpen", open);
    }
}
