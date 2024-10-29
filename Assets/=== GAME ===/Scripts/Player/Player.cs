using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] List<AnimationController> anims = new List<AnimationController>();
    [SerializeField] List<PlayerData> playerDatas = new List<PlayerData>();

    [HideInInspector] public AnimationController currentAnims;
    [HideInInspector] public PlayerData currentPlayer;
    private void Awake()
    {
        currentAnims = anims[0];
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

    [ContextMenu("Set Dead")]
    public void SetDeadPlayer() => currentAnims.PlayAnimDead();
}
