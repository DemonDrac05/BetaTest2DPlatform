using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface GotDamaged
{
    float GotDamagedTime { get; set; }
    float GotDamagedDuration { get; set; }
    float DyingTime { get; set; }
    float DyingDuration { get; set; }
    void RestartLevel();
}
