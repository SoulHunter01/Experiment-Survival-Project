using UnityEngine;

public abstract class EnemyCombat: MonoBehaviour{
    
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    // [SerializeField] protected float speed;
    [SerializeField] protected float stamina;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackRange;

    // protected void BaseTakeDamage(float damage){
    //     health -= damage;
    //     if(health <= 0){
    //         Die();
    //     }
    // }
    public virtual void TakeDamage(float damage){
        // BaseTakeDamage(damage);
        health -= damage;
        if(health <= 0){
            Die();
        }
    }

    protected void Die(){
        //implement
        Debug.Log("Enemy died");
    }

}