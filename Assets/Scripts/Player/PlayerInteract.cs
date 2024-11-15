using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.EventSystems;

public class PlayerInteract : MonoBehaviour
{

    // private Camera cam;
    // [SerializeField]
    // private float distance = 3f;
    // [SerializeField]
    // private LayerMask layerMask;
    // private Interactable activeInteractable;
    // scripts:
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
        // if(inputManager.CurrentActionMap == inputManager.standing.Get()){
        //     Ray ray = new(cam.transform.position, cam.transform.forward);
        //     // Physics.Raycast(ray, out RaycastHit hit, distance);
        //     playerUI.UpdateText(string.Empty);
        //     // Debug.Log(ray);
        //     // Debug.DrawRay(ray.origin, ray.direction * distance);
        //     RaycastHit hit;
        //     if(Physics.Raycast(ray, out hit,distance,layerMask)){
        //         //get interactable in a variable:
        //         Interactable interactable = hit.collider.GetComponent<Interactable>();
        //         // Debug.Log("hit");
        //         // Debug.Log(hit.collider.name);
        //         // Debug.Log(interactable);
        //         if(interactable != null){
        //             playerUI.UpdateText(interactable.PromptMessage);
        //             if(inputManager.standing.Interact.triggered){
        //                 interactable.BaseInteract();
        //             }
        //         }
        //     }
        // }
        // else if(inputManager.CurrentActionMap == inputManager.sitting.Get()){
        //     if(inputManager.sitting.Stand.triggered){
        //         inputManager.ActiveBench.BaseInteract();
        //     }
        // }

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
        // else if(playerRaycaster.getEnemy(out EnemyCombat enemy)){
        //     playerUI.UpdateText("Press E to attack");
        //     if(inputManager.standing.Interact.triggered){
        //         // enemy.BaseInteract();
        //     }
        // }
    }

    private void SittingInteractions(){
        playerUI.UpdateText("Press E to stand up");
        if(inputManager.sitting.Stand.triggered){
            inputManager.ActiveBench.BaseInteract();
        }
    }
}