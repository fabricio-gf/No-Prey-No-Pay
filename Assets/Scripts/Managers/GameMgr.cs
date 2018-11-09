using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    public float m_gameOverDuration = 5;
    public AnimationCurve m_gameOverSlowdown = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1,0));

    public static float Timer       { get; private set; }
    public static float DeltaTime   { get; private set; }

    public static bool IsPaused     { get; private set; }
    public static bool IsGameOver   { get; private set; }
    public static bool IsRoundEnd   { get; private set; }
    public static float TimeRatio   { get; private set; }

    private static GameMgr m_manager;

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Start()
    {
        Debug.Assert(m_manager == null, this.gameObject.name + " - GameMgr : must be unique!");
        m_manager = this;
        PlayGame();
        TimeRatio = 1f;
        IsRoundEnd = false;
    }

    // ======================================================================================
    void Update()
    {
        if (!IsPaused)
        {
            DeltaTime = TimeRatio * Time.deltaTime;
            Timer += DeltaTime;
        }
        else
        {
            DeltaTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //IsPaused = !IsPaused;
            if (IsPaused)
            {
                QuitGame();
            }
            else
            {
                IsPaused = true;
            }
        }
    }

    // ======================================================================================
    public static void QuitGame()
    {
        Application.Quit();
    }

    // ======================================================================================
    public static void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ======================================================================================
    public static void PauseGame()
    {
        IsPaused = true;
    }

    // ======================================================================================
    public static void PlayGame()
    {
        IsPaused = false;
        IsRoundEnd = false;
    }

    // ======================================================================================
    public static void EndRound()
    {
        m_manager.StartCoroutine(m_manager.StartEndRound());
    }

    // ======================================================================================
    public static void EndGame()
    {
        PauseGame();
        IsGameOver = true;
    }

    // ======================================================================================
    private IEnumerator StartEndRound()
    {
        float timer = m_gameOverDuration;

        while (timer > 0)
        {
            TimeRatio = m_gameOverSlowdown.Evaluate(1 - timer / m_gameOverDuration);
            yield return null;
            timer -= Time.deltaTime;
        }

        TimeRatio = 1.0f;
        PauseGame();
        IsRoundEnd = true;
    }
}
