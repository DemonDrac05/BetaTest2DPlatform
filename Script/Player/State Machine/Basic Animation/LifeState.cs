using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class LifeState : PlayerState
{
    public float getFlip = 1f;
    public Boss boss;
    public LifeState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        boss = FindObjectOfType<Boss>();
    }

    public override void EnterState()
    {
        base.EnterState();
        if(player.CurrentHealth > 0f)
        {
            player.GotDamagedTime = player.GotDamagedDuration;

            #region Check Flip Side
            if (boss.IsFacingRight) { getFlip = 1f; }
            else if (!boss.IsFacingRight) { getFlip = -1f; }
            #endregion
        }
        else player.DyingTime = player.DyingDuration;

    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        #region Hurt State
        if (player.CurrentHealth > 0f)
        {
            #region Main Function
            player.rb2d.velocity 
                = new Vector2(player.rb2d.velocity.x + (getFlip * 3), player.rb2d.velocity.y);

            player.AllowToFlip = false;
            player.GotDamagedTime -= Time.deltaTime;
            player.animator.Play("Hurt");
            #endregion

            #region Hurt -> Idle
            if (player.GotDamagedTime <= 0f)
            {
                player.GotDamagedTime = 0f;
                player.AllowToFlip = true;
                player.stateMachine.ChangeState(player.runState);
            }
            #endregion
        }
        #endregion

        #region Death State
        else
        {
            #region Main Function
            player.DyingTime -= Time.deltaTime;
            player.animator.Play("Death");

            if(player.DyingTime <= 0f)
            {
                player.DyingTime = 0f;
                player.gameObject.SetActive(false);
            }
            #endregion
        }
        #endregion
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
