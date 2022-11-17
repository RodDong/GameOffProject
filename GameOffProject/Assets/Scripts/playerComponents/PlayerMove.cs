using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject mPlayer;
    Vector2 mPos;
    Collider2D mPlayerCollider;
    Rigidbody2D mPlayerRigidBody;
    Transform mPlayerTransform;
    [SerializeField] GameObject battleUI;
    [SerializeField] GameObject InventoryUI, InventoryButton;
    Animator playerAnimator;

    enum State{
        Idle,
        Walk,
        Sit,
        Talk,
        Battle
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
        playerAnimator = gameObject.GetComponent<Animator>();
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
        if (mCurState == State.Talk) {
            return;
        }

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

    public void EnterDialogueMode() {
        if (mCurState == State.Talk) {
            Debug.LogError("Trying to enter dialogue mode when already in dialogue mode");
        } else {
            mCurState = State.Talk;
        }
    }

    public void ExitDialogueMode() {
        
        if (mCurState != State.Talk) {
            Debug.LogError("Trying to exit dialogue mode when not in dialogue mode");
        } else {
            mCurState = State.Idle;
        }
        mCurState = State.Battle;
    }

    void UpdatePlayerHorizontalVelocity(){
        if (mCurState == State.Talk) {
            return;
        }
        
        Vector2 tempV = mPlayerRigidBody.velocity;
        tempV.x = Input.GetAxisRaw("Horizontal") * mPlayerSpeed;
        mPlayerRigidBody.velocity = tempV;
    }

    void UpdateState(){
        switch (mCurState) {
            case State.Idle:
                UpdateIdle();
                break;
            case State.Walk:
                UpdateWalk();
                break;
            case State.Sit:
                UpdateSit();
                break;
            case State.Talk:
                UpdateTalk();
                break;
            case State.Battle:
                UpdateBattle();
                break;
        }
    }

    //TODO: update player based on player state
    void UpdateIdle(){
        InventoryButton.SetActive(true);
        if(mPlayerRigidBody.velocity != Vector2.zero){
            mCurState = State.Walk;
        }
        playerAnimator.Play("PlayerIdle");
    }

    void UpdateWalk(){
        InventoryButton.SetActive(true);
        if(mPlayerRigidBody.velocity == Vector2.zero){
            mCurState = State.Idle;
        }
        playerAnimator.Play("PlayerWalk");
    }

    void UpdateSit(){
        InventoryButton.SetActive(true);
    }

    void UpdateTalk() {
        InventoryUI.SetActive(false);
        InventoryButton.SetActive(false);
    }

    void UpdateBattle()
    {
        InventoryUI.SetActive(false);
        InventoryButton.SetActive(false);
        battleUI.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }

    
}
