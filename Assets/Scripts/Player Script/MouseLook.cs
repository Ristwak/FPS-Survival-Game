using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private Transform playerRoot, lookRoot;
    [SerializeField]
    private bool invert;
    [SerializeField]
    private bool can_Unlock = true;
    [SerializeField]
    private float sensitivity = 5f;
    [SerializeField]
    private int smooth_Steps = 10;
    [SerializeField]
    private float smooth_Weight = 0.4f;
    [SerializeField]
    private float roll_Angle = 10f;
    [SerializeField]
    private float roll_Speed = 3f;
    [SerializeField]
    private Vector2 defalt_look_Limits = new Vector2(-70f,80f);

    private Vector2 look_Angles;

    private Vector2 current_mouse_look;
    private Vector2 smooth_Move;

    private float current_Roll_Angle;

    private int last_Look_Frame;

    void Start()
    {
        // Locks the cursor to the center of the game window
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        LockAndUnlockCursor();

        if(Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    void LockAndUnlockCursor()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void LookAround()
    {
        // WE used MOUSE_Y in x-axis because in x-axis moving mouse makes the player look vertically if you move your mouse up-and-down then the movement will be smooth but if you move the mouse left-and-right the character will still look vertically up-and-down but now the movement wouldnt be smooth same case for y-axis
        current_mouse_look = new Vector2(Input.GetAxis(Mouse.MOUSE_Y),Input.GetAxis(Mouse.MOUSE_X));

        look_Angles.x += current_mouse_look.x * sensitivity * (invert ? 1f : -1f);
        look_Angles.y += current_mouse_look.y * sensitivity;

        // This will limit the look of the character as if you try to move the camera and it pasts the limit then it will not move any further
        look_Angles.x = Mathf.Clamp(look_Angles.x,defalt_look_Limits.x,defalt_look_Limits.y);

        // current_Roll_Angle =
        //     Mathf.Lerp(current_Roll_Angle, Input.GetAxisRaw(Mouse.MOUSE_X) * roll_Angle, Time.deltaTime * roll_Speed);

        lookRoot.localRotation = Quaternion.Euler(look_Angles.x, 0f, 0f);
        playerRoot.localRotation = Quaternion.Euler(0f, look_Angles.y, 0f);
    }
}
