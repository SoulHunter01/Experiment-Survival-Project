using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrGravesCombat : EnemyCombat
{
    
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Debug.Log("Dr. Graves took damage");
    }
}