using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface PlayerLife
{
    #region Hurt Variables
    float GotDamagedTime { get; set; }
    float GotDamagedDuration { get; set; }
    #endregion

    #region Death Variables
    float DyingTime { get; set; }
    float DyingDuration { get; set; }
    #endregion

    void RestartLevel();
}
