using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : BaseState
{
    // Start is called before the first frame update
    private float attackTimer;
    private float attackRate = 1.5f;
    private float attackRangeSqr = 9f;
    [SerializeField] private float damage = 10f;
    private PlayerHealth playerHealth;

    public override void Enter()
    {
        attackTimer = 0;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    public override void Execute()
    {
        float distSqr = (controller.GravesTransform.position - player.transform.position).sqrMagnitude;
        if(controller.PlayerVisible() && distSqr < attackRangeSqr){
            attackTimer += Time.deltaTime;
            if(attackTimer > attackRate){
                playerHealth.TakeDamage(damage, controller.GravesTransform.position, controller.EnemyUniqueID);
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
