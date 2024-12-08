using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private bool dead = false;
    [SerializeField] private Image healthBar;
    [SerializeField] private float maxHealth;
    [SerializeField] private DamageIndicator damageIndicator;
    [SerializeField] private Canvas damageIndicatorCanvas;
    [SerializeField] private Canvas deathCanvas;
    private JumpScare jumpScare;

    private Dictionary<float, DamageIndicator> activeDamageIndicators = new Dictionary<float, DamageIndicator>();
    void Start()
    {   
        jumpScare = ComponentUtil.getSafeComponent<JumpScare>(gameObject, "Player Health");
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
        if(dead) return;
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
        dead = true;
        Debug.Log("Player has died");
        jumpScare.TriggerJumpScare();

        StartCoroutine(ShowDeathScreen());
    }

    private IEnumerator ShowDeathScreen(){
        yield return new WaitForSeconds(1f);
        deathCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;

    }

    private void RetryLevel(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
