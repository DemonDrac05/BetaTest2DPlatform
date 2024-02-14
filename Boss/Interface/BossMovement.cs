using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BossMovement
{
    #region Horizontal Movement
    bool IsFacingRight { get; set; }
    float moveSpeed { get; set; }
    void BossMovement(Vector2 velocity);
    void CheckForLeftOrRightFacing(Vector2 velocity);
    #endregion

    #region Vertical Movement
    float jumpForce { get; set; }
    short jumpStep { get; set; }
    #endregion
}
