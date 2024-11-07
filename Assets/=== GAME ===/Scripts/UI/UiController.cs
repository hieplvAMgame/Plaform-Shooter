using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class UiController : Singleton<UiController>
{
    [SerializeField] public CharacterHPBar bar;
    [SerializeField] Transform parent;
    public void AddBar(int maxVal,Transform transform, out CharacterHPBar result)
    {
        CharacterHPBar newBar = Instantiate(bar, parent);
        newBar.Init(maxVal, transform);
        result = newBar;
    }
}
