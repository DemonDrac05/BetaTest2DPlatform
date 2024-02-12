using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BossTriggerCheck
{
    bool isAggroed { get; set; }
    bool isInNormalAttackDistance { get; set; }
    bool isInUltiAttackDistance { get; set; }
    void SetAggroStatus(bool Aggroed);
    void SetNormalAttackDistance(bool InNormalAttackDistance);
    void SetUltiAttackDistance(bool InUltiAttackDistance);
}
