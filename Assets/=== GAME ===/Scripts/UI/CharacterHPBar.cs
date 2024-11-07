using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CharacterHPBar : MonoBehaviour
{
    [SerializeField] Image hpBar;
    [SerializeField] Text txtName;
    Transform target;
    [SerializeField] float offset;

    [Space]
    //[SerializeField] Image reloadFill;
    [SerializeField] Image fireRateFill;
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
            hpBar.DOFillAmount((float)val / maxVal, .3f);
        }
    }
    private void Update()
    {
        if (!target)
            Destroy(gameObject);
    }
    private void LateUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.position + Vector3.up * offset);
    }
    public void UpdateBarUI(float fireRateTime)
    {
        fireRateFill.gameObject.SetActive(fireRateTime > 0);
        fireRateFill.fillAmount = fireRateTime;
    }
}
