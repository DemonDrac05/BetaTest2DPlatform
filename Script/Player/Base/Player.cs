using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Player : MonoBehaviour, PlayerComponents, PlayerStatic, PlayerLife, PlayerMovement, PlayerAttack, PlayerAttackTrigger
{
    #region Player Components
    public Rigidbody2D rb2d { get; set; }
    public Animator animator { get; set; }
    public BoxCollider2D bc2d { get; set; }
    [field: SerializeField] public LayerMask jumpableGround { get; set; }
    [field: SerializeField] public LayerMask climbableGround { get; set; }
    #endregion

    #region Player Statics

    #region Player Health
    [field: SerializeField] public Slider healthSlider { get; set; }
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }
    #endregion

    #region Player Mana
    [field: SerializeField] public Slider manaSlider { get; set; }
    [field: SerializeField] public float MaxMana { get; set; } = 400f;
    public float CurrentMana { get; set; }
    #endregion

    #endregion

    #region Player Animation
    [Header("Animation")]
    public string playerIdle = "Player_Idle";
    public string playerRun = "Player_Running";
    public string playerJump = "Player_Jump";
    public string playerFall = "Player_Fall";
    public string playerDoubleJump = "Player_DoubleJump";
    public string playerWallHold = "Player_Climb";
    #endregion

    #region State Machine Variables
    public PlayerStateMachine stateMachine { get; set; }
    public HorizontalState runState { get; set; }
    public JumpState jumpState { get; set; }
    public WallHolding wallHoldState { get; set; }
    public NormalAttack attackState { get; set; }
    public LifeState lifeState { get; set; }

    #region CheckpointState
    public CheckpointIn cpIn { get; set; }
    public CheckpointOut cpOut { get; set; }
    public CheckpointProgress cpProgress { get; set; }
    #endregion

    #endregion

    #region Movement (Left & Right)

    #region Variables
    [field: SerializeField] public float moveSpeed { get; set; } = 5f;
    public bool AllowToFlip { get; set; } = true;
    public bool IsFacingRight { get; set; } = true;
    public float posX; public float posY; public Vector3 flip;
    #endregion

    #region Function
    public void movementInput(out float horizontal,out float vertical)
    {
        #region Block Movement
        if (!AllowToFlip)
        {
            horizontal = 0f;
            vertical = 0f;
        }
        #endregion

        #region Allow Movement
        else
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }
        #endregion
    }
    public void CheckForLeftOrRightFacing()
    {
        #region Block Flipping
        if (!AllowToFlip) return;
        #endregion

        #region Allow Flipping
        IsFacingRight = !IsFacingRight;

        flip = transform.localScale; flip.x = -1;

        if(flip.x > 0) { transform.Rotate(0, 0, 0); }
        else if(flip.x < 0) { transform.Rotate(0, 180, 0); }
        #endregion
    }
    #endregion

    #endregion

    #region Terrain Setup

    #region Variables
    [field: SerializeField] public float jumpForce { get; set; } = 14f;
    public short jumpStep { get; set; }
    public bool isHoldingWall { get; set; } = true;
    #endregion

    #region CheckTerrain
    public bool IsGrounded()
    {
        return Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    
    public bool IsClimbable(float minDistance, float maxDistance)
    {
        float rangeLimitDistance = Mathf.Clamp(maxDistance,minDistance,maxDistance);

        bool rightClimb = CheckDirection(Vector2.right, rangeLimitDistance);
        bool leftClimb = CheckDirection(Vector2.left, rangeLimitDistance);

        return rightClimb || leftClimb;
    }

    public bool CheckDirection(Vector2 direction, float maxDistance)
    {
        RaycastHit2D hit = Physics2D.Raycast(bc2d.bounds.center, direction, maxDistance, climbableGround);

        if(hit.collider != null)
        {
            return Mathf.Abs(hit.point.x - bc2d.bounds.center.x) <= maxDistance;
        }

        return false;
    }
    #endregion

    #endregion

    #region Attack Variables
    public float attackTime { get; set; }
    [field: SerializeField] public float attackDuration { get; set; } = 0.5f;
    [field: SerializeField] public float attackCD { get; set; } = 0.6f;
    public float attackCDtime { get; set; }

    #region Trigger Check
    public bool InRangeAttack { get; set; }
    public void SetInRangeAttack(bool inRangeAttack)
    {
        InRangeAttack = inRangeAttack;
    }
    #endregion

    #endregion

    #region Got Damaged Variables
    public float GotDamagedTime { get; set; }
    [field: SerializeField] public float GotDamagedDuration { get; set; } = 0.4f;
    public float DyingTime { get; set; }
    [field: SerializeField] public float DyingDuration { get; set; } = 1f;
    public bool IsDead { get; set; }
    #endregion

    public Boss boss;
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        #region State Variables Declaration
        runState = new HorizontalState(this, stateMachine);
        jumpState = new JumpState(this, stateMachine);
        wallHoldState = new WallHolding(this, stateMachine);
        attackState = new NormalAttack(this, stateMachine);
        lifeState = new LifeState(this, stateMachine);

        #region CheckpointState
        cpIn = new CheckpointIn(this, stateMachine);
        cpOut = new CheckpointOut(this, stateMachine);
        cpProgress = new CheckpointProgress(this, stateMachine);
        #endregion

        #endregion
    }
    private void Start()
    {
        #region Statics Set Up

        #region Health
        CurrentHealth = MaxHealth;
        healthSlider.maxValue = MaxHealth;
        healthSlider.value = CurrentHealth;
        #endregion

        #region Mana
        CurrentMana = MaxMana;
        manaSlider.maxValue = MaxMana;
        manaSlider.value = CurrentMana;
        #endregion

        #endregion

        #region Player GetComponents
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bc2d = GetComponent<BoxCollider2D>();
        #endregion

        #region Player Basic Variables
        flip.x = 1; jumpStep = 2; isHoldingWall = false;
        attackCDtime = attackCD;
        GotDamagedTime = GotDamagedDuration;
        #endregion

        stateMachine.Initialize(runState);
    }
    private void FixedUpdate()
    {
        #region Flip Condition
        if (posX > 0 && !IsFacingRight)
        {
            CheckForLeftOrRightFacing();
        }
        else if(posX < 0 && IsFacingRight)
        {
            CheckForLeftOrRightFacing();
        }
        #endregion

        stateMachine.CurrentPlayerState.PhysicsUpdate();
    }
    private void Update()
    {
        boss = FindObjectOfType<Boss>();

        #region Player Movement
        movementInput(out posX,out posY);
        rb2d.velocity = new Vector2(posX * moveSpeed, rb2d.velocity.y);
        #endregion

        #region Player Attack
        if (attackCDtime > 0f) attackCDtime -= Time.deltaTime;
        if (attackCDtime <= 0f) attackCDtime = 0f;
        #endregion

        #region Player Static

        #region Condition For Player Health
        if (CurrentHealth <= 0f)
        {
            CurrentHealth = 0f;
        }
        if (CurrentHealth < healthSlider.value)
        {
            stateMachine.ChangeState(lifeState);
            healthSlider.value = CurrentHealth;
        }
        #endregion

        #region Condition For Player Mana
        manaSlider.value = CurrentMana;
        #endregion

        #endregion

        stateMachine.CurrentPlayerState.FrameUpdate();
    }

    #region Health && Die

    #region Got Damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        #region Got Damage By Enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (CurrentHealth >= 10f && boss.CurrentHealth > 0f)
            {
                CurrentHealth -= 10f;
            } 
        }
        #endregion

        #region Got Damage by Trap
        #endregion
    }
    #endregion

    #region Restart After Death
    public void RestartLevel()
    {
    }
    #endregion

    #endregion
}
