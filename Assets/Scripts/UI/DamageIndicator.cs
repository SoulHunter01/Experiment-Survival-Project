using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 damageSrcPos;
    [SerializeField]private Transform playerTrans;
    [SerializeField]private Transform damageIndicatorTrans;

    private CanvasGroup DamageImageCG;
    private float fadeStartTime, fadeTime, maxFadeTime;
    private float damageSourceID;

    void Start()
    {   
        fadeStartTime = 0.5f;
        maxFadeTime = fadeTime = 1f;
        DamageImageCG = ComponentUtil.getSafeComponent<CanvasGroup>(gameObject, "DamageIndicator");
        if(!playerTrans){
            playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
            if(!playerTrans){
                Debug.LogError("Player not found");
            }
        }

        if(!damageIndicatorTrans){
            damageIndicatorTrans = transform;
            if(!damageIndicatorTrans){
                Debug.LogError("Damage Indicator not found");
            }
        }
    }

    // Update is called once per frame
    void Update(){
        if(damageSrcPos == Vector3.zero){
            // Debug.LogError("Damage source position not set");
            return;
        }
        if(fadeStartTime > 0){
            fadeStartTime -= Time.deltaTime;
        }
        else{
            fadeTime -= Time.deltaTime;
            DamageImageCG.alpha = fadeTime / maxFadeTime;
            if(fadeTime <= 0){
                Destroy(gameObject);
            }
        }
        Vector3 Direction = damageSrcPos - playerTrans.position;
        float angle = Vector3.SignedAngle(Direction, playerTrans.forward, Vector3.up);
        damageIndicatorTrans.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void SetDamageSrcPos(Vector3 pos, float enemyID){
        damageSrcPos = pos;
        damageSourceID = enemyID;
    }

    public float GetDamageSourceID(){
        return damageSourceID;
    }

}
