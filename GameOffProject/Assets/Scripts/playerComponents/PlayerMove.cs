using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject mPlayer;
    Vector2 mPos;
    Collider2D mPlayerCollider;
    Rigidbody2D mPlayerRigidBody;
    Transform mPlayerTransform;

    enum State{
        Idle,
        Walk,
        Sit
    }

    float deltaTime;
    [SerializeField]float mPlayerSpeed = 3.0f;
    //true is right, false is left, start with left
    bool faceRight;
    State mCurState = State.Idle;


    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        mPlayerTransform = mPlayer.GetComponent<Transform>();
        mPos = mPlayerTransform.position;
        mPlayerCollider = mPlayer.GetComponent<Collider2D>();
        mPlayerRigidBody = mPlayer.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        deltaTime = Time.deltaTime;
        Vector3 playerScale = mPlayerTransform.localScale;

        faceRight = playerScale.x < 0;

        ProcessInput(deltaTime);
        UpdatePlayerHorizontalVelocity();
        UpdateState();
    }

    void ProcessInput(float deltaTime){

        Vector2 mPlayerScale = mPlayerTransform.localScale;

        //A to move left, D to move right
        if(Input.GetKey(KeyCode.A)){
            if(faceRight){
                mPlayerScale.x *= -1;
            }
            mCurState = State.Walk;
        }
        else if(Input.GetKey(KeyCode.D)){
            if(!faceRight){
                mPlayerScale.x *= -1;
            }
            mCurState = State.Walk;
        }

        //update player rotation using scaleX
        mPlayerTransform.localScale = mPlayerScale;
    }

    void UpdatePlayerHorizontalVelocity(){
        Vector2 tempV = mPlayerRigidBody.velocity;
        tempV.x = Input.GetAxisRaw("Horizontal") * mPlayerSpeed;
        mPlayerRigidBody.velocity = tempV;
    }

    void UpdateState(){
        if(mCurState == State.Idle){
            UpdateIdle();
        }
        if(mCurState == State.Walk){
            UpdateWalk();
        }
        if(mCurState == State.Sit){
            UpdateSit();
        }
    }

    //TODO: update player based on player state
    void UpdateIdle(){
        if(mPlayerRigidBody.velocity != Vector2.zero){
            mCurState = State.Walk;
        }
    }

    void UpdateWalk(){
        if(mPlayerRigidBody.velocity == Vector2.zero){
            mCurState = State.Idle;
        }
    }

    void UpdateSit(){

    }
}
