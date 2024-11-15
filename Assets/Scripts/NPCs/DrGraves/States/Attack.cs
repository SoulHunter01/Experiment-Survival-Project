using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : BaseState
{
    // Start is called before the first frame update
    private float attackTimer;
    private float attackRate = 1.5f;
    private float damage = 10;
    private PlayerHealth playerHealth;

    public override void Enter()
    {
        attackTimer = 0;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    public override void Execute()
    {
        if(controller.PlayerVisible()){
            attackTimer += Time.deltaTime;
            if(attackTimer > attackRate){
                playerHealth.TakeDamage(damage);
                attackTimer = 0;
            }
        }
        else{
            stateMachine.SwitchState(new Chase());
        }
    }

    public override void Exit()
    {
        
    }
}
