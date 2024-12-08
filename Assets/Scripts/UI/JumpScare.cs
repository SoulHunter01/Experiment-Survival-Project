using System.Collections;
using UnityEngine;

class JumpScare:MonoBehaviour{
    
    [SerializeField] private GameObject jumpScarePrefab;
    [SerializeField] private Camera mainCamera;
    private float moveSpeed = 10f;
    // private float startDistance = 5f;
    private float finalDistance = 1.5f;
    private float jumpscareDuration = 1.5f;
    private Vector3 startRelativeToCam;
    
    public void TriggerJumpScare(){
        Debug.Log("Jumpscare triggered");
        startRelativeToCam = (Vector3.forward * 5f) + (Vector3.down * 1.5f);
        Vector3 startPosition = mainCamera.transform.position +startRelativeToCam;
        Debug.Log("Start position: " + startPosition);
        GameObject jumpScareEnemy = Instantiate(jumpScarePrefab,startPosition, Quaternion.identity);
        jumpScareEnemy.transform.position = startPosition;
        jumpScareEnemy.transform.LookAt(mainCamera.transform);

        StartCoroutine(JumpScareAnimation(jumpScareEnemy));

    }

    private IEnumerator JumpScareAnimation(GameObject enemy){
        float timer = 0;
        while(timer < jumpscareDuration){
            Debug.Log(timer);
            float t = timer/jumpscareDuration;
            Vector3 target = mainCamera.transform.position + (mainCamera.transform.forward * finalDistance);
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, target, t);
            enemy.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.5f, t);
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(enemy);
    }
}