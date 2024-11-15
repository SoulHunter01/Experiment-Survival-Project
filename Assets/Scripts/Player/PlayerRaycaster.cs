using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycaster : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private float defaultDistance = 3f;
    [SerializeField] private LayerMask layerMask;

    
    private RaycastHit hit;
    private bool rayHit;
    void Start()
    {
        mainCamera = GetComponent<PlayerLook>().cam;
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
    }

    private void CastRay(){
        Ray ray = new(mainCamera.transform.position, mainCamera.transform.forward);

        rayHit = Physics.Raycast(ray, out hit, defaultDistance, layerMask);
    }

    public bool getInteractable(out Interactable interactable){
        interactable = null;
        if(!rayHit) return false;
        
        interactable = hit.collider.GetComponent<Interactable>();
        if(interactable != null){
            // Debug.Log("Layer :" + hit.collider.gameObject.layer);
            return interactable != null;
        }
        else{
            return false;
        }

    }

    public bool getEnemy(out EnemyCombat enemy ){
        enemy = null;
        // Debug.Log("ray hit" +rayHit);
        if(!rayHit) return false;
        // Debug.Log("Layer :" + hit.collider.gameObject.layer);

        enemy = hit.collider.GetComponent<EnemyCombat>();
        return enemy != null;
    }

    // public bool isLookingAtObject(out RaycastHit hit, float customDistance = 0){
        
    // }
}
