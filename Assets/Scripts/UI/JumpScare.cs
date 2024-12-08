using System.Collections;
using UnityEngine;

class JumpScare : MonoBehaviour
{
    [SerializeField] private GameObject jumpScarePrefab;
    [SerializeField] private Camera mainCamera;
    private float finalDistance = 1.5f;
    private float jumpscareDuration = 1.5f;
    private Vector3 startRelativeToCam;
    
    public void TriggerJumpScare()
    {
        Debug.Log("Jumpscare triggered");
        startRelativeToCam = (Vector3.forward * 5f) + (Vector3.down * 1.5f);
        Vector3 startPosition = mainCamera.transform.position + startRelativeToCam;
        
        GameObject jumpScareEnemy = Instantiate(jumpScarePrefab, startPosition, Quaternion.identity);
        
        // Destroy animator if present
        Animator enemAnim = jumpScareEnemy.GetComponent<Animator>();
        if (enemAnim)
        {
            Destroy(enemAnim);
        }

        // More precise facing method
        Vector3 directionToCamera = (mainCamera.transform.position - jumpScareEnemy.transform.position).normalized;
        jumpScareEnemy.transform.rotation = Quaternion.LookRotation(directionToCamera);

        StartCoroutine(JumpScareAnimation(jumpScareEnemy));
    }

    private IEnumerator JumpScareAnimation(GameObject enemy)
    {
        float timer = 0f;
        Vector3 startPosition = enemy.transform.position;
        while (timer < jumpscareDuration)
        {
            float t = timer / jumpscareDuration;
            Vector3 target = mainCamera.transform.position + (mainCamera.transform.forward * finalDistance);
            
            // Smooth movement and scaling
            enemy.transform.position = Vector3.Lerp(startPosition, target, t);
            enemy.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.5f, t);
            
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(enemy);
    }
}