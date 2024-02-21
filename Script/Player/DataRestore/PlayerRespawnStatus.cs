using UnityEngine;

public class PlayerRespawnStatus : MonoBehaviour
{
    public Player player;

    public CameraMovement camera;

    public SetCheckPoint checkPoint;

    private float respawnTime = 0f;

    [field: SerializeField] private float respawnDuration = 2f;

    [field: SerializeField] public float adjustableWidth;

    [field: SerializeField] public float adjustableHeight;

    private void Awake()
    {
        player = FindObjectOfType<Player>();

        camera = FindObjectOfType<CameraMovement>();

        checkPoint = FindObjectOfType<SetCheckPoint>();
    }

    private void Start()
    {
        respawnTime = 0f;
    }

    private void Update()
    {
        if (player.CurrentHealth <= 0f && respawnTime <= 0f)
        {
            respawnTime = respawnDuration;
        }

        if (respawnTime > 0f)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        respawnTime -= Time.deltaTime;

        if (respawnTime <= 0f)
        {
            respawnTime = 0f;

            PlayerStatusAfterRespawn();
        }
    }

    void PlayerStatusAfterRespawn()
    {
        player.gameObject.SetActive(true);

        Static();

        Coordination();

        StateMachine();
    }

    void Static()
    {
        player.CurrentHealth = player.MaxHealth;
        player.healthSlider.value = player.CurrentHealth;
    }

    void Coordination()
    {
        #region Player
        player.transform.position = new Vector3(
                checkPoint.playerCheckPoint.x + adjustableWidth,
                checkPoint.playerCheckPoint.y + adjustableHeight, 
                player.transform.position.z);
        #endregion

        #region Camera
        camera.maxPosition.x = checkPoint.playerCheckPoint.x + checkPoint.distanceFromCheckpointToRight;
        camera.minPosition.x = checkPoint.playerCheckPoint.x -
          checkPoint.distanceFromCheckpointToLeft;
        #endregion
    }

    void StateMachine()
    {
        player.stateMachine.ChangeState(player.jumpState);
    }
}
