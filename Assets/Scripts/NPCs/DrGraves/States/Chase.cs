using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : BaseState
{   
    private float losePlayerTimer;
    private float moveTimer;
    // private float attackRange = 5f;
    private float attackRangeSqr = 25f;

    public override void Enter()
    {
        
    }

    public override void Execute()
    {

        //keeps chasing player until player is out of range
        if(controller.PlayerVisible()){
            losePlayerTimer = 0;
            controller.Agent.SetDestination(player.transform.position);
            //if distance is less than 5, attack
            float distSquared = (controller.transform.position - player.transform.position).sqrMagnitude;
            if(distSquared < attackRangeSqr){
                stateMachine.SwitchState(new Attack());
            }
        }
        else{
            losePlayerTimer += Time.deltaTime;
            if(losePlayerTimer > 1){
                stateMachine.SwitchState(new Confused());
            }
        }
    }

    public override void Exit()
    {
        
    }
}
