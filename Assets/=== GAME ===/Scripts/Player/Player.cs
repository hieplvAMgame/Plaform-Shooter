using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] List<AnimationController> anims = new List<AnimationController>();
    [SerializeField] List<PlayerData> playerDatas = new List<PlayerData>();

    [Space]
    [SerializeField] Weapon weapon;

    [HideInInspector] public AnimationController currentAnims;
    [HideInInspector] public PlayerData currentPlayer;
    CharacterHPBar hpBar;

    public bool IsShoot { get => GetComponent<PlayerInput>().IsShoot; }

    public string Tag => gameObject.tag;

    private void Awake()
    {
        currentAnims = anims[0];
        SetupCharacter(0);
    }

    public void SetupCharacter(int id)
    {
        currentAnims = anims[id];
        currentAnims.PlayAnimIdle();

        currentPlayer = playerDatas[id];
        currentPlayer.InitPlayer(() =>
        {
            Debug.Log("Player Die!");
        });
    }
    IEnumerator Start()
    {
        yield return new WaitUntil(() => UiController.Instance);
        UiController.Instance.AddBar(currentPlayer.maxHP, transform, out hpBar);
        weapon.RegistWithUIBar(hpBar);
    }

    [ContextMenu("Set Dead")]
    public void SetDeadPlayer() => currentAnims.PlayAnimDead();

    public void TakeDamage(int damage)
    {
        currentPlayer.ReduceHP(damage);
        hpBar.ChangeValue(currentPlayer.CurrentHP);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GAME_TAG.Picker))
        {
            collision.TryGetComponent(out WeaponPicker picker);
            picker.OnClaimPicker(out int id);
            weapon.SelectWeapon(id);
        }
    }
}
