using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UniRx;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private int _flyTime;

    public event Action<LevelData> OnLoadLevelEvent;
       
    public IReactiveProperty<int> PlayerHp => _player.CurrentHp;
    public IReactiveProperty<float> FlyProgress { get; private set; }
    public IReadOnlyReactiveProperty<bool> Lose => PlayerHp.Select(t => t <= 0).ToReactiveProperty();
    public IReadOnlyReactiveProperty<bool> Win => FlyProgress.Select(t => t >= 1).ToReactiveProperty();

    private float _flyProgress;

    private void Start()
    {
        Time.timeScale = 1;

        FlyProgress = new ReactiveProperty<float>(0);
        
        var levelData = GameProgress.SelectedLevel;
        Assert.IsNotNull(levelData);

        OnLoadLevelEvent?.Invoke(levelData);

        Lose.Where(t => t == true).Subscribe(t => PauseGame());                
        Win.Where(t => t == true).Subscribe(t => WinByFly());

        Observable.EveryUpdate().Subscribe(t =>
        {
            _flyProgress += Time.deltaTime;           
            FlyProgress.Value = _flyProgress / _flyTime;
        });
    }   

    private void WinByFly()
    {           
        PauseGame();

        GameProgress.SaveProgress();        
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }
}
