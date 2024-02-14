using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerAttackTrigger
{
    bool InRangeAttack { get; set; }
    void SetInRangeAttack(bool inRangeAttack);
}
