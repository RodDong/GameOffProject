using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    GameObject mPlayer;
    Vector2 mPos;
    Collider2D mPlayerCollider;
    Rigidbody2D mPlayerRigidBody;
    Transform mPlayerTransform;
    PlayerStatus mPlayerStatus;
    [SerializeField] GameObject battleUI;
    [SerializeField] GameObject inventoryUI, inventoryButton;
    [SerializeField] GameObject cluesUI, cluesButton;
    [SerializeField] BattleManager battleManager;
    [SerializeField] public AudioClip walkingClip, walkingOutsideClip;
    Animator playerAnimator;
    AudioSource walkingAudio;

    public enum State {
        Idle,
        Walk,
        Sit,
        Talk,
        Battle,
        UseInventory,
        UseClues
    }

    float deltaTime;
    float mPlayerSpeed = 0.0f;
    float PLAYER_MAX_SPEED = 4.0f;
    float PLAYER_ACCELERATION = 4.0f;
    //true is right, false is left, start with left
    bool faceRight;
    [SerializeField] State mCurState = State.Idle;

    public void SetCurState(State state) { mCurState = state; }
    public State GetCurState() { return mCurState; }
    public bool CanUseInteractables() { 
        return mCurState == State.Idle || mCurState == State.Walk || mCurState == State.Sit; 
    }

    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        mPlayerTransform = mPlayer.GetComponent<Transform>();
        mPos = mPlayerTransform.position;
        mPlayerCollider = mPlayer.GetComponent<Collider2D>();
        mPlayerRigidBody = mPlayer.GetComponent<Rigidbody2D>();
        playerAnimator = gameObject.GetComponent<Animator>();
        mPlayerStatus = gameObject.GetComponent<PlayerStatus>();
        walkingAudio = gameObject.GetComponent<AudioSource>();
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
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            mCurState = State.Idle;
        }
        else if (Input.GetKey(KeyCode.A)){
            if(faceRight){
                mPlayerScale.x *= -1;
            }
            if (!walkingAudio.isPlaying) {
                walkingAudio.Play();
                mCurState = State.Walk;
            }
        }
        else if(Input.GetKey(KeyCode.D)){
            if(!faceRight){
                mPlayerScale.x *= -1;
            }
            if (!walkingAudio.isPlaying) {
                walkingAudio.Play();
                mCurState = State.Walk;
            }
        }

        //update player rotation using scaleX
        mPlayerTransform.localScale = mPlayerScale;
    }

    public void EnterDialogueMode() {
        walkingAudio.time = 0.0f;
        walkingAudio.Stop();
        if (mCurState == State.Talk || mCurState == State.Battle) {
            Debug.LogError("Trying to enter dialogue mode when already in dialogue/battle mode");
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
    }

    public void EnterBattleMode() {
        mCurState = State.Battle;
        mPlayerStatus.ResetCurrentHealth();
        battleManager.SetBattleState(BattleManager.State.Battle);
        battleManager.ResetBattleVisuals();
        battleManager.DeactivateGameObjectsInScene();
        mPlayer.GetComponent<SpriteRenderer>().enabled = false;
    }

    void UpdatePlayerHorizontalVelocity(){
        if (mCurState == State.Talk
            || mCurState == State.Battle
            || mCurState == State.UseInventory
            || mCurState == State.UseClues)
        {
            return;
        }

        if(mPlayerSpeed < PLAYER_MAX_SPEED)
        {
            mPlayerSpeed += PLAYER_ACCELERATION * deltaTime;
        }
        Vector2 tempV = mPlayerRigidBody.velocity;
        tempV.x = Input.GetAxisRaw("Horizontal") * mPlayerSpeed;
        mPlayerRigidBody.velocity = tempV;

        playerAnimator.speed = Mathf.Lerp(0.5f, 1.0f, mPlayerSpeed / PLAYER_MAX_SPEED);

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
        mPlayerSpeed = 0.0f;
        inventoryButton.SetActive(true);
        cluesButton.SetActive(true);
        if (mPlayerRigidBody.velocity != Vector2.zero) {
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
        if (mPlayerRigidBody.velocity == Vector2.zero) {
            walkingAudio.time = 0.0f;
            walkingAudio.Stop();
            mCurState = State.Idle;
        }
        if (inventoryUI.activeSelf)
        {
            walkingAudio.time = 0.0f;
            walkingAudio.Stop();
            mCurState = State.UseInventory;
        }
        else if (cluesUI.activeSelf)
        {
            walkingAudio.time = 0.0f;
            walkingAudio.Stop();
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
        battleManager.UpdateEquippedMask();
        battleManager.UpdateSkillButtons();
        battleManager.UpdatePlayerStatVisual();
    }

    void UpdateUseInventory()
    {
        if (!inventoryUI.activeSelf)
        {
            mCurState = State.Idle;
        }
        playerAnimator.Play("PlayerIdle");
    }

    public void OpenInventory()
    {
        if (!inventoryUI.activeSelf)
        {
            cluesUI.SetActive(false);
        }
    }

    public void OpenClues()
    {
        if (!cluesUI.activeSelf)
        {
            inventoryUI.SetActive(false);
        }
    }

    void UpdateuseClues()
    {
        if (!cluesUI.activeSelf)
        {
            mCurState = State.Idle;
        }
        playerAnimator.Play("PlayerIdle");
    }
    
}
