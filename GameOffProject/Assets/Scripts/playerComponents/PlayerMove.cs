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
    [SerializeField] GameObject inventoryUI, inventoryButton;
    [SerializeField] GameObject cluesUI, cluesButton;
    [SerializeField] BattleManager battleManager;
    Animator playerAnimator;

    enum State{
        Idle,
        Walk,
        Sit,
        Talk,
        Battle,
        UseInventory,
        UseClues
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
        if (mCurState == State.Talk 
            || mCurState == State.Battle 
            || mCurState == State.UseInventory 
            || mCurState == State.UseClues) {
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
        if (mCurState == State.Talk
            || mCurState == State.Battle
            || mCurState == State.UseInventory
            || mCurState == State.UseClues)
        {
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
            case State.UseInventory:
                UpdateUseInventory();
                break;
            case State.UseClues:
                UpdateuseClues();
                break;
        }
    }

    //TODO: update player based on player state
    void UpdateIdle(){
        inventoryButton.SetActive(true);
        cluesButton.SetActive(true);
        if (mPlayerRigidBody.velocity != Vector2.zero){
            mCurState = State.Walk;
        }
        if (inventoryUI.activeSelf)
        {
            mCurState = State.UseInventory;
        }else if (cluesUI.activeSelf)
        {
            mCurState = State.UseClues;
        }
        playerAnimator.Play("PlayerIdle");
    }

    void UpdateWalk(){
        inventoryButton.SetActive(true);
        cluesButton.SetActive(true);
        if (mPlayerRigidBody.velocity == Vector2.zero){
            mCurState = State.Idle;
        }
        if (inventoryUI.activeSelf)
        {
            mCurState = State.UseInventory;
        }
        else if (cluesUI.activeSelf)
        {
            mCurState = State.UseClues;
        }
        playerAnimator.Play("PlayerWalk");
        
    }

    void UpdateSit(){
        inventoryButton.SetActive(true);
        cluesButton.SetActive(true);
    }

    void UpdateTalk() {
        playerAnimator.Play("PlayerIdle");
        inventoryUI.SetActive(false);
        inventoryButton.SetActive(false);
        cluesUI.SetActive(false);
        cluesButton.SetActive(false);
    }

    void UpdateBattle()
    {
        inventoryUI.SetActive(false);
        inventoryButton.SetActive(false);
        cluesUI.SetActive(false);
        cluesButton.SetActive(false);
        battleUI.SetActive(true);
        transform.parent.gameObject.SetActive(false);
        battleManager.UpdateEquippedMask();
        battleManager.UpdateSkillButtons();
        battleManager.UpdatePlayerStatVisual();
    }

    void UpdateUseInventory()
    {
        playerAnimator.Play("PlayerIdle");
        cluesUI.SetActive(false);
        cluesButton.SetActive(false);
        if (!inventoryUI.activeSelf)
        {
            mCurState = State.Idle;
        }
    }

    void UpdateuseClues()
    {
        playerAnimator.Play("PlayerIdle");
        inventoryUI.SetActive(false);
        inventoryButton.SetActive(false);
        if (!cluesUI.activeSelf)
        {
            mCurState = State.Idle;
        }
    }

    
}
