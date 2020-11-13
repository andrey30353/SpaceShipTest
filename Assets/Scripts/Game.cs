using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private int _flyTime;

    public event Action<LevelData> OnLoadLevelEvent;

    public event Action<int> OnPlayerChangeHpEvent;
    public event Action<float> OnFlyProgressChangeEvent;
    public event Action OnLoseEvent;
    public event Action OnWinEvent;

    private float _flyProgress;

    private void Start()
    {
        Time.timeScale = 1;

        var levelData = GameProgress.SelectedLevel;
        Assert.IsNotNull(levelData);

        OnLoadLevelEvent?.Invoke(levelData);
    }

    private void OnEnable()
    {
        _player.OnChangeHpEvent += Player_OnChangeHpEvent;
    }   

    private void OnDisable()
    {
        _player.OnChangeHpEvent -= Player_OnChangeHpEvent;
    }

    private void Update()
    {
        _flyProgress += Time.deltaTime;
        OnFlyProgressChangeEvent?.Invoke(_flyProgress / _flyTime);

        CheckWin();
    }

    private void CheckWin()
    {
        if (_flyProgress >= _flyTime)
        {
            OnWinEvent?.Invoke();
            PauseGame();

            GameProgress.SaveProgress();           
        }
    }   

    private void Player_OnChangeHpEvent(int currentHp)
    {
        OnPlayerChangeHpEvent?.Invoke(currentHp);

        if(currentHp <= 0)
        {
            Destroy(_player.gameObject);

            GameOver();
        }        
    }

    private void GameOver()
    {
        OnLoseEvent?.Invoke();
        PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }
}
