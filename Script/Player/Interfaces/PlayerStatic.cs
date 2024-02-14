using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface PlayerStatic
{
    #region Player Health
    Slider healthSlider { get; set; }
    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }
    #endregion

    #region Player Mana
    Slider manaSlider { get; set; }
    float MaxMana { get; set; }
    float CurrentMana { get; set; }
    #endregion
}
