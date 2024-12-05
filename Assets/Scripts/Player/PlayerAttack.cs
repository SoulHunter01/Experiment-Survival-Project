using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerRaycaster raycaster;
    private InputManager inputManager;
    [SerializeField] private float attackCooldown = 1f;
    private float lastAttackTime;

    void Awake(){
        raycaster = ComponentUtil.getSafeComponent<PlayerRaycaster>(gameObject, "PlayerAttack");
        inputManager = ComponentUtil.getSafeComponent<InputManager>(gameObject, "PlayerAttack");


        lastAttackTime = Time.time;
    }

    void Update(){
        // if(inputManager.CurrentActionMap == inputManager.standing.Get()){
        //     if(inputManager.standing.Attack.triggered){

        //         if(attackTimer > attackCooldown){
        //             Attack();
        //             attackTimer = 0;
        //         }
        //     }
        // }
        // attackTimer += Time.deltaTime;
    }

    public void Attack(){

        if(Time.time - lastAttackTime < attackCooldown) return;
        Debug.Log("Attacking");

        if(raycaster.getEnemy(out EnemyCombat enemy)){
            Debug.Log("Enemy Hit");
            enemy.TakeDamage(10);
            lastAttackTime = Time.time;
        }
    }
}
