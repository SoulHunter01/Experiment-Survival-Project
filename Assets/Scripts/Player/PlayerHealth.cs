using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;

    [SerializeField] private Image healthBar;
    [SerializeField] private float maxHealth;
    [SerializeField] private DamageIndicator damageIndicator;
    [SerializeField] private Canvas damageIndicatorCanvas;

    private Dictionary<float, DamageIndicator> activeDamageIndicators = new Dictionary<float, DamageIndicator>();
    void Start()
    {
        if(healthBar == null){
            Debug.LogError("Health bar is not assigned");
        }
        if(maxHealth <= 0){
            Debug.LogWarning("Max health is not set, setting to 100");
            maxHealth = 100f;
        }
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        // Debug.Log("Health: " + fillAm);
    }

    public void TakeDamage(float damage, Vector3 damagePos, float enemyUniqueID){

        health -= damage;
        if(health <= 0){
            Die();
            return;
        }
        if(activeDamageIndicators.ContainsKey(enemyUniqueID)){
            if(activeDamageIndicators[enemyUniqueID] != null){
                Destroy(activeDamageIndicators[enemyUniqueID].gameObject);
            }
            activeDamageIndicators.Remove(enemyUniqueID);
        }

        DamageIndicator indicator = Instantiate(damageIndicator, damageIndicatorCanvas.transform);
        indicator.SetDamageSrcPos(damagePos, enemyUniqueID);
        //set active:
        indicator.gameObject.SetActive(true);
        activeDamageIndicators[enemyUniqueID] = indicator;
        
    }
    private void OnDisable() {
        foreach(var indicator in activeDamageIndicators.Values){
            if(indicator != null){
                Destroy(indicator.gameObject);
            }
        }
        activeDamageIndicators.Clear();
    }

    private void Die(){
        Debug.Log("Player has died");
    }
}
