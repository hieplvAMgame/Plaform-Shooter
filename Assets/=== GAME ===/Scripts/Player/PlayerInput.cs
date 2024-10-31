using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] KeyCode keymapLeft, keymapRight, keymapFire, keymapJump;


    [SerializeField] float acceleration = 2f;
    float horizontal = 0;
    public float Horizontal
    {
        get
        {
            if (Input.GetKey(keymapLeft) || Input.GetKey(KeyCode.LeftArrow))
            {
                horizontal = Mathf.MoveTowards(horizontal, -1, acceleration * Time.deltaTime);
            }
            else if (Input.GetKey(keymapRight) || Input.GetKey(KeyCode.RightArrow))
            {
                horizontal = Mathf.MoveTowards(horizontal, 1, acceleration * Time.deltaTime);
            }
            else
            {
                horizontal = Mathf.MoveTowards(horizontal, 0f, acceleration * Time.deltaTime);
            }
            return horizontal;
        }
    }
    public bool IsJump => Input.GetKey(keymapJump);

    public bool IsShoot => Input.GetMouseButton(0) || Input.GetKey(keymapFire);

    public Vector3 MousePos => Camera.main.ScreenToWorldPoint(Input.mousePosition);
}
