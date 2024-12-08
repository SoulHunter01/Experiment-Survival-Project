using UnityEngine;

public abstract class BaseState{

    public GravesController controller;
    public StateMachine stateMachine;
    public GameObject player;
    // public Animator stateAnimator;
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
