using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoWeapon : MonoBehaviour,IWeapons{             //這個腳本用在沒有選擇武器到左下欄位的時候
    public Sprite icon;
    public Sprite Icon { get { return icon; } set { icon = value; } }

    public void DisableEffects()
    {

    }
}
