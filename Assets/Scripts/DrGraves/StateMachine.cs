using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState currentState;

    public void Initialize (){
        SwitchState(new Patrol());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState != null){
            currentState.Execute();
        }
    }

    public void SwitchState(BaseState newState){
        if(currentState != null){
            currentState.Exit();
        }
        currentState = newState;
        if(currentState != null){
            currentState.stateMachine = this;
            currentState.player = GameObject.FindGameObjectWithTag("Player");

            currentState.controller = GetComponent<GravesController>();
            if(currentState.controller == null){
                Debug.LogError("State Machine could not find the enemy controller");
                return;
            }
            currentState.Enter();

        }
    }
}
