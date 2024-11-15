using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected string promptMessage;
    public string PromptMessage=>promptMessage;

    public void BaseInteract(){
        Debug.Log(promptMessage);
        Interact();
    }
    protected virtual void Interact(){
        Debug.Log("Interacting with base class");
    }
}
