using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Game _game;

    [SerializeField] private Slider _flyProgressSlider;

    [SerializeField] private GameObject _loseMessage;
    [SerializeField] private GameObject _winMessage;

    //[SerializeField] private Button _restartButton;
    //[SerializeField] private Button _mapButton;

    private LivePanel _livePanel;

    private void Awake()
    {
        _livePanel = GetComponentInChildren<LivePanel>();
    }

    private void Start()
    {
        _game.PlayerHp.Subscribe(_livePanel.UpdateUI);
        _game.FlyProgress.Subscribe(UpdateFlyProgressSlider);

        _game.Win.Where(t => t == true).Subscribe(t => { ShowWinMessage(); });
        _game.Lose.Where(t => t == true).Subscribe(t => { ShowLoseMessage(); });

        //_restartButton.onClick.AsObservable().Subscribe(t => RestartLevel());
        //_mapButton.onClick.AsObservable().Subscribe(t => LoadMapScene());
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMapScene()
    {
        SceneManager.LoadScene(0);
    }

    private void UpdateFlyProgressSlider(float progress)
    {
        _flyProgressSlider.value = progress;
    }

    private void ShowLoseMessage()
    {
        _loseMessage.SetActive(true);
    }

    private void ShowWinMessage()
    {
        _winMessage.SetActive(true);
    }

}
