using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WeaponPicker : MonoBehaviour
{
    int idPicker;

    [Button("Show Picker")]
    public void ShowPicker(int idWeapon)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == idWeapon)
                transform.GetChild(i).gameObject.SetActive(true);
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }
        idPicker = idWeapon;
    }

    public void OnClaimPicker(out int id)
    {
        id = idPicker;
        gameObject.SetActive(false);
    }
}
