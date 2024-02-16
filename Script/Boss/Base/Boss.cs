using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour, BossComponents, BossStatic, BossLife, BossMovement, BossTriggerCheck
{
    #region Boss Components
    public Rigidbody2D rb2d { get; set; }
    public Animator animator { get; set; }
    public BoxCollider2D bc2d { get; set; }
    [field: SerializeField] public LayerMask jumpableGround { get; set; }
    #endregion

    #region Boss Statics
    [field: SerializeField] public Slider healthSlider { get; set; }
    [field: SerializeField] public float MaxHealth { get; set; } = 1000f;
    public float CurrentHealth { get; set; }
    #endregion

    #region Terrain Setup

    #region Variables
    [field: SerializeField] public float jumpForce { get; set; } = 14f;
    public short jumpStep { get; set; }
    #endregion

    #region CheckTerrain
    public bool IsGrounded()
    {
        return Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    #endregion

    #endregion

    #region Boss Movement

    #region Variables
    [field: SerializeField] public float moveSpeed { get; set; } = 10f;
    public bool IsFacingRight { get; set; } = true;
    #endregion

    #region Movement Function
    public void BossMovement(Vector2 velocity)
    {
        rb2d.velocity = velocity;
        CheckForLeftOrRightFacing(velocity);
    }
    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        if (IsFacingRight && velocity.x < 0f) 
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }

        else if(!IsFacingRight && velocity.x > 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
    }
    #endregion

    #endregion

    #region Trigger Check

    #region Variables
    public bool checkInAttackRange { get; set; }
    public bool isAggroed { get; set; }
    public bool isInNormalAttackDistance { get; set; }
    public bool isInUltiAttackDistance { get; set; }
    #endregion

    #region Check Function
    public void SetInAttackRange(bool inAttackRange)
    {
        checkInAttackRange = inAttackRange;
    }
    public void SetAggroStatus(bool Aggroed)
    {
        isAggroed = Aggroed;
    }
    public void SetNormalAttackDistance(bool InNormalAttackDistance)
    {
        isInNormalAttackDistance = InNormalAttackDistance;
    }
    public void SetUltiAttackDistance(bool InUltiAttackDistance)
    {
        isInUltiAttackDistance = InUltiAttackDistance;
    }
    #endregion

    #endregion

    #region Boss State Machine Variables
    public BossStateMachine stateMachine { get; set; }
    public BossLifeState lifeState { get; set; }
    public BossIdle idleState { get; set; }
    public BossChase chaseState { get; set; }
    public BossJump jumpState { get; set; }
    public BossAttack attackState { get; set; }
    public BossUltimate ultimateState { get; set; }
    #endregion

    #region Boss Attack Variables
    public float attackTimer { get; set; }
    public float attackDuration { get; set; } = 1f;
    #endregion

    #region Boss Got Damaged Variables
    public float GotDamagedTime { get; set; }
    public float GotDamagedDuration { get; set; } = 0.2f;
    public float DyingTime { get; set; }
    public float DyingDuration { get; set; } = 2f;
    #endregion

    public Player player;

    private void Awake()
    {
        stateMachine = new BossStateMachine();

        lifeState = new BossLifeState(this, stateMachine);
        idleState = new BossIdle(this, stateMachine);
        chaseState = new BossChase(this, stateMachine);
        jumpState = new BossJump(this, stateMachine);
        attackState = new BossAttack(this, stateMachine);
        ultimateState = new BossUltimate(this, stateMachine);
    }
    private void Start()
    {
        #region Boss Components
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bc2d = GetComponent<BoxCollider2D>();
        #endregion

        #region Boss Static
        CurrentHealth = MaxHealth;
        healthSlider.maxValue = MaxHealth;
        healthSlider.value = CurrentHealth;
        #endregion

        stateMachine.Initialize(idleState);
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentBossState.PhysicsUpdate();
    }

    private void Update()
    {
        player = FindObjectOfType<Player>();

        #region Boss Health Static
        if (CurrentHealth < healthSlider.value)
        {
            stateMachine.ChangeState(lifeState);
            healthSlider.value = CurrentHealth;
        }
        #endregion

        #region After Kill -> Idle
        if (player.CurrentHealth <= 0f)
        {
            stateMachine.ChangeState(idleState);
        }
        #endregion

        stateMachine.CurrentBossState.FrameUpdate();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
