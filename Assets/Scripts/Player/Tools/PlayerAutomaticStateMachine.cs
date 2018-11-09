using UnityEngine;

//[RequireComponent(typeof(PlayerAnimatorController))]
public class PlayerAutomaticStateMachine : MonoBehaviour
{
    // -------------------------------- PUBLIC AUX STRUCT -------------------------------- //
    [System.Serializable]
    public struct sAutomaticState
    {
        public PlayerAnimatorController.eStates     m_state;
        public PlayerAnimatorController.eAttackType m_attkType;
        public PlayerAnimatorController.eDirections m_facingDir;
        public float                                m_upAttkDir;
        public float                                m_duration;
    }

    // --------------------------------- PUBLIC ATTRIBUTES ------------------------------- //
    public sAutomaticState[]            m_states;


    private PlayerAnimatorController    m_animator;
    private int                         m_actualState;
    private float                       m_actualTimer;


    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Start()
    {
        m_animator      = this.gameObject.GetComponent<PlayerAnimatorController>();

        m_actualState   = m_states.Length - 1;
        m_actualTimer   = 0;
    }

    // ======================================================================================
    public void Update()
    {
        m_actualTimer -= Time.deltaTime;

        if (m_actualTimer <= 0)
        {
            m_actualState++;
            m_actualState = m_actualState < m_states.Length ? m_actualState : 0;
            m_actualTimer = m_states[m_actualState].m_duration;
        }
        
        UpdateAnimator();
    }

    // ======================================================================================
    private void UpdateAnimator()
    {
        m_animator.SetState(m_states[m_actualState].m_state);
        m_animator.SetDirection(m_states[m_actualState].m_facingDir);
        m_animator.StartAttack(m_states[m_actualState].m_attkType, m_states[m_actualState].m_upAttkDir, false);
    }
}
