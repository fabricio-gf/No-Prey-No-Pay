using UnityEngine;
using XInputDotNetPure; // Required in C#

public class InputMgr : MonoBehaviour
{
    // --------------------------------------- ENUMS ------------------------------------- //
    public enum eAxis
    {
        HORIZONTAL,
        VERTICAL
    }

    public enum eButton
    {
        DASH,
        JUMP,
        TOSS,
        ATTACK,
        GRAB
    }

    public enum eXBoxButton
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

    // --------------------------------- PUBLIC ATTRIBUTES ------------------------------- //
    public eXBoxButton m_dashButton;

    public eXBoxButton m_jumpButton;

    public eXBoxButton m_tossButton;
    public eXBoxButton m_attackButton;
    public eXBoxButton m_grabButton;

    public float       m_triggMinRatio = .3f;

    public bool        m_pcDebugMode = false;

    // --------------------------------- PRIVATE ATTRIBUTES ------------------------------ //
    private static InputMgr m_manager;


    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Start()
    {
        if (!Application.isEditor)
            m_pcDebugMode = false;

        Debug.Assert(m_manager == null, this.gameObject.name + " - InputMgr : Mgr must be unique!");
        m_manager = this;
    }

    // ======================================================================================
    public static bool GetButton(int _player, eButton _button)
    {
#if UNITY_EDITOR
        if (m_manager.m_pcDebugMode && _player == 1)
            return GetDebugButton(_button);
#endif

        if (_player > 4 || _player <= 0)
            return false;

        GamePadState gamePadState = GamePad.GetState((PlayerIndex)(_player - 1));

        switch (_button)
        {
            case eButton.ATTACK:
                return GetButton(gamePadState, m_manager.m_attackButton);
            case eButton.DASH:
                return GetButton(gamePadState, m_manager.m_dashButton);
            case eButton.GRAB:
                return GetButton(gamePadState, m_manager.m_grabButton);
            case eButton.TOSS:
                return GetButton(gamePadState, m_manager.m_tossButton);
            case eButton.JUMP:
                return GetButton(gamePadState, m_manager.m_jumpButton);
        }

        return false;
    }

    // ======================================================================================
    public static float GetAxis(int _player, eAxis _axis)
    {
#if UNITY_EDITOR
        if (m_manager.m_pcDebugMode && _player == 1)
            return GetDebugAxis(_axis);
#endif

        if (_player > 4 || _player <= 0)
            return 0f;

        GamePadState gamePadState = GamePad.GetState((PlayerIndex)(_player - 1));

        switch (_axis)
        {

            case eAxis.HORIZONTAL:
                return gamePadState.ThumbSticks.Left.X;
            case eAxis.VERTICAL:
                return gamePadState.ThumbSticks.Left.Y;
        }

        return 0f;
    }


    // ======================================================================================
    // PRIVATE METHODS
    // ======================================================================================
    private static bool GetButton(GamePadState _gamePadState, eXBoxButton _xboxButton)
    {
        switch (_xboxButton)
        {
            // TRIGGERS AS BUTTONS
            case eXBoxButton.TRIGG_LEFT:
                return _gamePadState.Triggers.Left > m_manager.m_triggMinRatio;
            case eXBoxButton.TRIGG_RIGHT:
                return _gamePadState.Triggers.Right > m_manager.m_triggMinRatio;

            // BUTTONS
            case eXBoxButton.A:
                return _gamePadState.Buttons.A == ButtonState.Pressed;
            case eXBoxButton.B:
                return _gamePadState.Buttons.B == ButtonState.Pressed;
            case eXBoxButton.X:
                return _gamePadState.Buttons.X == ButtonState.Pressed;
            case eXBoxButton.Y:
                return _gamePadState.Buttons.Y == ButtonState.Pressed;
            case eXBoxButton.BUMPR_LEFT:
                return _gamePadState.Buttons.LeftShoulder == ButtonState.Pressed;
            case eXBoxButton.BUMPR_RIGHT:
                return _gamePadState.Buttons.RightShoulder == ButtonState.Pressed;
            case eXBoxButton.STICK_LEFT:
                return _gamePadState.Buttons.LeftStick == ButtonState.Pressed;
            case eXBoxButton.STICK_RIGHT:
                return _gamePadState.Buttons.RightStick == ButtonState.Pressed;
            case eXBoxButton.START:
                return _gamePadState.Buttons.Start == ButtonState.Pressed;
            case eXBoxButton.OPTIONS:
                return _gamePadState.Buttons.Guide == ButtonState.Pressed;
            case eXBoxButton.DPAD_UP:
                return _gamePadState.DPad.Up == ButtonState.Pressed;
            case eXBoxButton.DPAD_DOWN:
                return _gamePadState.DPad.Down == ButtonState.Pressed;
            case eXBoxButton.DPAD_LEFT:
                return _gamePadState.DPad.Left == ButtonState.Pressed;
            case eXBoxButton.DPAD_RIGHT:
                return _gamePadState.DPad.Right == ButtonState.Pressed;
        }

        return false;
    }


    // ======================================================================================
    private static bool GetDebugButton (eButton _button)
    {
        switch (_button)
        {
            case eButton.ATTACK:
                return Input.GetKey(KeyCode.A);
            case eButton.DASH:
                return Input.GetKey(KeyCode.F);
            case eButton.GRAB:
                return Input.GetKey(KeyCode.D);
            case eButton.TOSS:
                return Input.GetKey(KeyCode.S);
            case eButton.JUMP:
                return Input.GetKey(KeyCode.Space);
        }

        return false;
    }

    // ======================================================================================
    private static float GetDebugAxis (eAxis _axis)
    {
        switch (_axis)
        {
            case eAxis.HORIZONTAL:
                return (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) - (Input.GetKey(KeyCode.LeftArrow) ? 1 : 0);
            case eAxis.VERTICAL:
                return (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) - (Input.GetKey(KeyCode.DownArrow) ? 1 : 0);
        }

        return 0f;
    }
}
