using System;
using UnityEngine;

public class Confused : BaseState{

    private float confusionTime = 4f;
    private float confusionTimer;
    private float goalAngle;
    private float turnRateScalar = 90f;
    private float currAngle;
    public override void Enter()
    {
        confusionTimer = 0;
        currAngle = controller.GravesTransform.eulerAngles.y;
        // goalAngle = currAngle + (90.0f * (UnityEngine.Random.value > 0.5f ? 1f : -1f));
        float signAngle = UnityEngine.Random.value > 0.5f ? 1f:-1f;
        goalAngle = currAngle + (90f*signAngle);
    }
    public override void Execute()
    {   
        confusionTimer += Time.deltaTime;
        if(confusionTimer > confusionTime){
            stateMachine.SwitchState(new Patrol());
        }


        if(!controller.PlayerVisible()){
            //
            currAngle = controller.GravesTransform.eulerAngles.y;
            
            //now rotate towards goal angle using turnRateScalar:
            float angleDiff = goalAngle - currAngle;
            float turnRate = turnRateScalar * Time.deltaTime;
            if( Math.Abs(angleDiff)< 5f){
                controller.GravesTransform.eulerAngles = new Vector3(0, goalAngle, 0);
                // stateMachine.SwitchState(new Patrol());
                goalAngle = newGoal(currAngle);
            }
            else{
                controller.GravesTransform.eulerAngles = new Vector3(0, currAngle + Mathf.Sign(angleDiff) * turnRate, 0);

            }

        }
        else{
            stateMachine.SwitchState(new Chase());
            
        }
    }
    public override void Exit()
    {
        // throw new System.NotImplementedException();
    }

    private float newGoal(float currAngle){
        float signAngle = UnityEngine.Random.value>0.5f?1f:-1f;
        return currAngle + (90f *signAngle);
    }
}