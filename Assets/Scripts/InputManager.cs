using UnityEngine;
using XInputDotNetPure; // Required in C#

public class InputManager : MonoBehaviour
{
    // --------------------------------------- ENUMS ------------------------------------- //
    public enum Axis
    {
        HORIZONTAL,
        VERTICAL
    }

    public enum Buttons
    {
        DASH,
        JUMP,
        TOSS,
        ATTACK,
        GRAB,
        START,
        SELECT,
        BACK,
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    public enum XBoxButtons
    {
        A,
        B,
        X,
        Y,
        DPAD_LEFT,
        DPAD_RIGHT,
        DPAD_UP,
        DPAD_DOWN,
        START,
        OPTIONS,
        BUMPR_LEFT,
        TRIGG_LEFT,
        STICK_LEFT,
        BUMPR_RIGHT,
        TRIGG_RIGHT,
        STICK_RIGHT,
    }

    // --------------------------------------- STRUCT ------------------------------------ //
    //[System.Serializable]
    //public struct sPlayerInput
    //{
    //    public string m_horizontalAxis;
    //    public string m_verticalAxis;

    //    public string m_dashButton;

    //    public string m_jumpButton;

    //    public string m_tossButton;
    //    public string m_attackButton;
    //    public string m_grabButton;
    //}
    
    public XBoxButtons m_dashButton;

    public XBoxButtons m_jumpButton;

    public XBoxButtons m_tossButton;
    public XBoxButtons m_attackButton;
    public XBoxButtons m_grabButton;
    public XBoxButtons m_startButton;
    public XBoxButtons m_selectButton;
    public XBoxButtons m_backButton;
    public XBoxButtons m_leftButton;
    public XBoxButtons m_rightButton;
    public XBoxButtons m_upButton;
    public XBoxButtons m_downButton;

    public float m_triggMinRatio = .3f;
    

    // --------------------------------- PUBLIC ATTRIBUTES ------------------------------- //
    //public sPlayerInput[]   m_playerInputs;
    

    // --------------------------------- PRIVATE ATTRIBUTES ------------------------------ //
    private static InputManager m_manager;

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Start()
    {
        Debug.Assert(m_manager == null, this.gameObject.name + " - InputManager : input manager must be unique!");
        m_manager = this;
    }
    
    // ======================================================================================
    public static bool GetButton(int _player, Buttons _button)
    {
        if (_player > 4 || _player <= 0)
            return false;

        GamePadState gamePadState = GamePad.GetState( (PlayerIndex) (_player - 1) );

        switch (_button)
        {
            case Buttons.ATTACK:
                return GetButton(gamePadState, m_manager.m_attackButton);
            case Buttons.DASH:
                return GetButton(gamePadState, m_manager.m_dashButton);
            case Buttons.GRAB:
                return GetButton(gamePadState, m_manager.m_grabButton);
            case Buttons.TOSS:
                return GetButton(gamePadState, m_manager.m_tossButton);
            case Buttons.JUMP:
                return GetButton(gamePadState, m_manager.m_jumpButton);
            case Buttons.START:
                return GetButton(gamePadState, m_manager.m_startButton);
            case Buttons.SELECT:
                return GetButton(gamePadState, m_manager.m_selectButton);
            case Buttons.BACK:
                return GetButton(gamePadState, m_manager.m_backButton);
            case Buttons.LEFT:
                return GetButton(gamePadState, m_manager.m_leftButton);
            case Buttons.RIGHT:
                return GetButton(gamePadState, m_manager.m_rightButton);
            case Buttons.UP:
                return GetButton(gamePadState, m_manager.m_upButton);
            case Buttons.DOWN:
                return GetButton(gamePadState, m_manager.m_downButton);
        }

        return false;
    }

    public static bool GetButton(Buttons _button)
    {

        for(int i = 0; i < 4; i++){
            GamePadState gamePadState = GamePad.GetState((PlayerIndex)i);

            switch (_button)
            {
                case Buttons.ATTACK:
                    return GetButton(gamePadState, m_manager.m_attackButton);
                case Buttons.DASH:
                    return GetButton(gamePadState, m_manager.m_dashButton);
                case Buttons.GRAB:
                    return GetButton(gamePadState, m_manager.m_grabButton);
                case Buttons.TOSS:
                    return GetButton(gamePadState, m_manager.m_tossButton);
                case Buttons.JUMP:
                    return GetButton(gamePadState, m_manager.m_jumpButton);
                case Buttons.START:
                    return GetButton(gamePadState, m_manager.m_startButton);
                case Buttons.SELECT:
                    return GetButton(gamePadState, m_manager.m_selectButton);
                case Buttons.BACK:
                    return GetButton(gamePadState, m_manager.m_backButton);
                case Buttons.LEFT:
                    return GetButton(gamePadState, m_manager.m_leftButton);
                case Buttons.RIGHT:
                    return GetButton(gamePadState, m_manager.m_rightButton);
                case Buttons.UP:
                    return GetButton(gamePadState, m_manager.m_upButton);
                case Buttons.DOWN:
                    return GetButton(gamePadState, m_manager.m_downButton);
            }
        }

        return false;
    }

    // ======================================================================================
    public static float GetAxis(int _player, Axis _axis)
    {
        if (_player > 4 || _player <= 0)
            return 0f;

        GamePadState gamePadState = GamePad.GetState( (PlayerIndex) (_player - 1) );

        switch (_axis)
        {

            case Axis.HORIZONTAL:
                return gamePadState.ThumbSticks.Left.X;
            case Axis.VERTICAL:
                return gamePadState.ThumbSticks.Left.Y;
        }

        return 0f;
    }

    public static float GetAxis(Axis _axis)
    {
        for(int i = 0; i < 4; i++){
            GamePadState gamePadState = GamePad.GetState((PlayerIndex)i);

            switch (_axis)
            {
                case Axis.HORIZONTAL:
                    return gamePadState.ThumbSticks.Left.X;
                case Axis.VERTICAL:
                    return gamePadState.ThumbSticks.Left.Y;
            }
        }

        return 0f;
    }


    // ======================================================================================
    // PRIVATE METHODS
    // ======================================================================================
    private static bool GetButton(GamePadState _gamePadState, XBoxButtons _xboxButton)
    {
        switch (_xboxButton)
        {
            // TRIGGERS AS BUTTONS
            case XBoxButtons.TRIGG_LEFT:
                return _gamePadState.Triggers.Left > m_manager.m_triggMinRatio;
            case XBoxButtons.TRIGG_RIGHT:
                return _gamePadState.Triggers.Right > m_manager.m_triggMinRatio;

            // BUTTONS
            case XBoxButtons.A:
                return _gamePadState.Buttons.A == ButtonState.Pressed;
            case XBoxButtons.B:
                return _gamePadState.Buttons.B == ButtonState.Pressed;
            case XBoxButtons.X:
                return _gamePadState.Buttons.X == ButtonState.Pressed;
            case XBoxButtons.Y:
                return _gamePadState.Buttons.Y == ButtonState.Pressed;
            case XBoxButtons.BUMPR_LEFT:
                return _gamePadState.Buttons.LeftShoulder == ButtonState.Pressed;
            case XBoxButtons.BUMPR_RIGHT:
                return _gamePadState.Buttons.RightShoulder == ButtonState.Pressed;
            case XBoxButtons.STICK_LEFT:
                return _gamePadState.Buttons.LeftStick == ButtonState.Pressed;
            case XBoxButtons.STICK_RIGHT:
                return _gamePadState.Buttons.RightStick == ButtonState.Pressed;
            case XBoxButtons.START:
                return _gamePadState.Buttons.Start == ButtonState.Pressed;
            case XBoxButtons.OPTIONS:
                return _gamePadState.Buttons.Guide == ButtonState.Pressed;
            case XBoxButtons.DPAD_UP:
                return _gamePadState.DPad.Up == ButtonState.Pressed;
            case XBoxButtons.DPAD_DOWN:
                return _gamePadState.DPad.Down == ButtonState.Pressed;
            case XBoxButtons.DPAD_LEFT:
                return _gamePadState.DPad.Left == ButtonState.Pressed;
            case XBoxButtons.DPAD_RIGHT:
                return _gamePadState.DPad.Right == ButtonState.Pressed;
        }

        return false;
    }

    //// ======================================================================================
    //public static bool GetButton(int _player, Buttons _button)
    //{
    //    if (_player > m_manager.m_playerInputs.Length || _player <= 0)
    //        return false;

    //    int i = _player - 1;

    //    switch (_button)
    //    {
    //        case Buttons.ATTACK:
    //            return Input.GetButtonDown(m_manager.m_playerInputs[i].m_attackButton);
    //        case Buttons.DASH:
    //            return Input.GetButtonDown(m_manager.m_playerInputs[i].m_dashButton);
    //        case Buttons.GRAB:
    //            return Input.GetButtonDown(m_manager.m_playerInputs[i].m_grabButton);
    //        case Buttons.TOSS:
    //            return Input.GetButtonDown(m_manager.m_playerInputs[i].m_tossButton);
    //        case Buttons.JUMP:
    //            return Input.GetButtonDown(m_manager.m_playerInputs[i].m_jumpButton);
    //    }

    //    return false;
    //}

    //// ======================================================================================
    //public static float GetAxis(int _player, Axis _axis)
    //{
    //    if (_player > m_manager.m_playerInputs.Length || _player <= 0)
    //        return 0f;

    //    int i = _player - 1;

    //    switch (_axis)
    //    {

    //        case Axis.HORIZONTAL:
    //            return Input.GetAxis(m_manager.m_playerInputs[i].m_horizontalAxis);
    //        case Axis.VERTICAL:
    //            return Input.GetAxis(m_manager.m_playerInputs[i].m_verticalAxis);
    //    }

    //    return 0f;
    //}
}
