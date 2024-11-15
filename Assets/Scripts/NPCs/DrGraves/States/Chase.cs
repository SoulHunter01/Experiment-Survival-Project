using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : BaseState
{   
    private float losePlayerTimer;
    private float moveTimer;
    private float attackRange = 2f;

    public override void Enter()
    {
        
    }

    public override void Execute()
    {
        if(controller.PlayerVisible()){
            losePlayerTimer = 0;
            controller.Agent.SetDestination(player.transform.position);
            //if distance is less than 2, attack
            float distSquared = (controller.transform.position - player.transform.position).sqrMagnitude;
            if(Vector3.Distance(controller.transform.position, player.transform.position) < attackRange){
                Debug.Log("Attacking");
                stateMachine.SwitchState(new Attack());
            }
        }
        else{
            losePlayerTimer += Time.deltaTime;
            if(losePlayerTimer > 5){
                stateMachine.SwitchState(new Patrol());
            }
        }
    }

    public override void Exit()
    {
        
    }
}
