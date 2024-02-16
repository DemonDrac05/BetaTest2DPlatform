using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BossTriggerCheck
{
    bool checkInAttackRange { get; set; }
    bool isAggroed { get; set; }
    bool isInNormalAttackDistance { get; set; }
    bool isInUltiAttackDistance { get; set; }
    void SetInAttackRange(bool inAttackRange);
    void SetAggroStatus(bool Aggroed);
    void SetNormalAttackDistance(bool InNormalAttackDistance);
    void SetUltiAttackDistance(bool InUltiAttackDistance);
}
