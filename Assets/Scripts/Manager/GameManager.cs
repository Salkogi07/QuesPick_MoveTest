using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 기존의 단순 변수 선언을 프로퍼티(get) 방식으로 변경
    public static GameManager instance
    {
        get
        {
            // 만약 instance가 비어있다면, 게임 세상에 있는 GameManager를 찾아서 넣는다.
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();
            }
            return _instance;
        }
    }

    // 실제 데이터를 담을 비공개 변수
    private static GameManager _instance;
    
    private void Awake()
    {
        // 싱글톤 중복 방지 (이미 있으면 나를 파괴)
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
    }

    public event Action<GameState> OnGameStateChanged;

    private GameState currentGameState = GameState.Gameplay;

    public void ChangeState(GameState state)
    {
        if (state == currentGameState)
            return;

        switch (state)
        {
            case GameState.UI:
                EnterUIState();
                break;
            case GameState.Gameplay:
                EnterGameplayState();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

        currentGameState = state;
        OnGameStateChanged?.Invoke(currentGameState);
    }

    private void EnterUIState()
    {
        Time.timeScale = 0f;
    }

    private void EnterGameplayState()
    {
        Time.timeScale = 1f;
    }

    public enum GameState
    {
        UI,
        Gameplay
    }
}