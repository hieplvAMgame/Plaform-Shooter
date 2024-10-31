using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHPBar : MonoBehaviour
{
    [SerializeField] Image hpBar;
    [SerializeField] Text txtName;
    Transform target;
    [SerializeField] float offset;
    int maxVal = 0;
    public void Init(int max, Transform target)
    {
        hpBar.fillAmount = 1;
        maxVal = max;
        this.target = target;
    }
    public void ChangeValue(int val)
    {
        if (val >= 0)
        {
            hpBar.fillAmount = (float)val / maxVal;
        }
    }
    private void Update()
    {
        if(!target)
            Destroy(gameObject);
    }
    private void LateUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.position + Vector3.up * offset);
    }
}
