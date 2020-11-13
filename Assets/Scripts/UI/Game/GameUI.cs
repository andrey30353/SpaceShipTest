using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Game _game;

    [SerializeField] private Slider _flyProgressSlider;

    [SerializeField] private GameObject _loseMessage;
    [SerializeField] private GameObject _winMessage;

    private LivePanel _livePanel;
      
    private void Awake()
    {
        _livePanel = GetComponentInChildren<LivePanel>();
    }

    private void OnEnable()
    {
        _game.OnPlayerChangeHpEvent += _livePanel.UpdateUI;       
        _game.OnFlyProgressChangeEvent += UpdateFlyProgressSlider;

        _game.OnLoseEvent += ShowLoseMessage;
        _game.OnWinEvent += ShowWinMessage;
    }

    
    private void OnDisable()
    {
        _game.OnPlayerChangeHpEvent -= _livePanel.UpdateUI;        
        _game.OnFlyProgressChangeEvent -= UpdateFlyProgressSlider;

        _game.OnLoseEvent -= ShowLoseMessage;
        _game.OnWinEvent += ShowWinMessage;
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
