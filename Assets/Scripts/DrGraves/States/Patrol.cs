using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : BaseState
{
    public int nodeIndex;
    public override void Enter()
    {
        // base.Enter();
        // Debug.Log("Patrol Enter");
    }

    public override void Execute()
    {
        PatrolCycle();
        if(controller.PlayerVisible())
        {
            stateMachine.SwitchState(new Chase());
        }
    }

    public override void Exit()
    {
        // base.Exit();
        // Debug.Log("Patrol Exit");
    }

    public void PatrolCycle()
    {
        if(controller.Agent.remainingDistance <= 0.1f)
        {
            if(nodeIndex < controller.path.nodes.Count - 1)
            {
                nodeIndex++;
            }
            else
            {
                nodeIndex = 0;
            }
            controller.Agent.SetDestination(controller.path.nodes[nodeIndex].position);
        }
    }
}
