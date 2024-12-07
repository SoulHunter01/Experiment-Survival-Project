using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GravesController : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private Transform gravesTransform;
    public Transform GravesTransform {get=>gravesTransform;}
    public NavMeshAgent Agent {get=>agent;}

    [SerializeField]
    private string currentState;
    public PatrollingPath path;
    private GameObject player;

    private float sightRange = 100f;
    private float sqrSightRange = 10000f;
    private float fov = 90f;

    //debug variables
    [SerializeField] private bool playerInFov = false;

    private float enemyUniqueID;
    public float EnemyUniqueID {get=>enemyUniqueID;}
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        gravesTransform = transform;
        player = GameObject.FindGameObjectWithTag("Player");

        enemyUniqueID = gameObject.GetInstanceID();

        stateMachine.Initialize();
    }

    void Update()
    {
        // PlayerVisible();
        // agent.SetDestination(player.position);
        currentState = stateMachine.currentState.ToString();
    }

    public float getPlayerAngle(){
        Vector3 forward = transform.forward;
        Vector3 diff = player.transform.position - transform.position;
        float angle = Vector3.Angle(diff, forward);
        return angle;
    }
    public bool PlayerInRange(float range){
        if(player == null) return false;
        Vector3 diff = player.transform.position - transform.position;
        float distance = diff.sqrMagnitude;
        if(distance > (range * range)) return false;
        return true;
    }
    public bool PlayerInFront (){
        if(player == null) return false;
        //check distance:
        Vector3 diff = player.transform.position - transform.position;
        float distance = diff.sqrMagnitude;
        if(distance > sqrSightRange) return false;

        Vector3 forward = transform.forward;
        float angle = Vector3.Angle(diff, forward);
        if(angle >= -fov && angle <= fov){
            // Debug.Log("Player is visible");
            //check env objects:
            Ray ray = new(transform.position, forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, sightRange)){
                if(hit.collider.gameObject == player){
                    return true;
                }
            }
        }
        playerInFov = false;
        return false;

    }
    public bool PlayerVisible(){
        
        float angle = getPlayerAngle();
        if(angle >= -fov && angle <= fov){
            Ray ray = new Ray(transform.position, player.transform.position - transform.position);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, sightRange)){
                if(hit.collider.gameObject == player){
                    return true;
                }
            }
        }
        return false;
    }
}
