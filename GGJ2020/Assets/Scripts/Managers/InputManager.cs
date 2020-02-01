using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    #region template
    public static bool get()
    {
        return false;
    }

    public static bool getUp()
    {
        return false;
    }

    public static bool getDown()
    {
        return false;
    }
    #endregion

    #region movement
    #region foward
    static KeyCode _forward = KeyCode.W;
    public static bool getMoveForward()
    {
        return Input.GetKey(_forward);
    }

    public static bool getMoveForwardUp()
    {
        return Input.GetKeyUp(_forward);
    }

    public static bool getMoveForwardDown()
    {
        return Input.GetKeyDown(_forward);
    }
    #endregion
    #region Back
    static KeyCode _back = KeyCode.S;
    public static bool getMoveBack()
    {
        return Input.GetKey(_back);
    }

    public static bool getMoveBackUp()
    {
        return Input.GetKeyUp(_back); ;
    }

    public static bool getMoveBackDown()
    {
        return Input.GetKeyDown(_back); ;
    }
    #endregion
    #region Left
    static KeyCode _left = KeyCode.A;
    public static bool getMoveLeft()
    {
        return Input.GetKey(_left);
    }

    public static bool getMoveLeftUp()
    {
        return Input.GetKeyUp(_left);
    }

    public static bool getMoveLeftDown()
    {
        return Input.GetKeyDown(_left);
    }
    #endregion
    #region Right
    static KeyCode _right = KeyCode.D;
    public static bool getMoveRight()
    {
        return Input.GetKey(_right);
    }

    public static bool getMoveRightUp()
    {
        return Input.GetKeyUp(_right);
    }

    public static bool getMoveRightDown()
    {
        return Input.GetKeyDown(_right);
    }
    #endregion
    #region Dash
    static KeyCode _dash = KeyCode.LeftShift;
    public static bool getDash()
    {
        return Input.GetKey(_dash);
    }

    public static bool getDashUp()
    {
        return Input.GetKeyUp(_dash);
    }

    public static bool getDashDown()
    {
        return Input.GetKeyDown(_dash);
    }
    #endregion
    #region Jump
    static KeyCode _jump = KeyCode.Space;
    public static bool getJump()
    {
        return Input.GetKey(_jump);
    }

    public static bool getJumpUp()
    {
        return Input.GetKeyUp(_jump);
    }

    public static bool getJumpDown()
    {
        return Input.GetKeyDown(_jump);
    }
    #endregion
    #endregion
    #region camera
    public static Vector2 getMouse()
    {
        return new Vector2(Input.GetAxis("MouseHorizontal"), Input.GetAxis("MouseVertical"));
    }
    #endregion
}
