using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface BossStatic
{
    Slider healthSlider { get; set; }
    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }
}
