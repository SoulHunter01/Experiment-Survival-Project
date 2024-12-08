using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    private PlayerRaycaster playerRaycaster;
    private PlayerUI playerUI;
    private InputManager inputManager;

    void Start()
    {
        
        // cam = GetComponent<PlayerLook>().cam;
        playerRaycaster = GetComponent<PlayerRaycaster>();
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
    

        if(inputManager.CurrentActionMap == inputManager.standing.Get()){
            StandingInteractions();
        }
        else if(inputManager.CurrentActionMap == inputManager.sitting.Get()){
            SittingInteractions();
        }
    }

    private void StandingInteractions(){
        playerUI.UpdateText(string.Empty);
        if(playerRaycaster.getInteractable(out Interactable interactable)){
            playerUI.UpdateText(interactable.PromptMessage);
            if(inputManager.standing.Interact.triggered){
                interactable.BaseInteract();
            }
        }

    }

    private void SittingInteractions(){
        playerUI.UpdateText("Press E to stand up");
        if(inputManager.sitting.Stand.triggered){
            inputManager.ActiveBench.BaseInteract();
        }
    }
}