using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    private LevelButton[] _levelButtons;

    private void Start()
    {
        _levelButtons = GetComponentsInChildren<LevelButton>();
        Debug.Log(_levelButtons.Length);
        GameProgress.Init(_levelButtons.Select(t=>t.LevelSettings));

        UpdateUI();
    }

    private void UpdateUI()
    {      
        foreach (var button in _levelButtons)
        {
            var enable = GameProgress.IsAvailableLevel(button.LevelSettings.Id);
            button.SetEnable(enable);
        }
    }

    public void StartLevel(LevelButton button)
    {
        var levelId = button.LevelSettings.Id;            
        GameProgress.SelectLevel(levelId);

        SceneManager.LoadScene(1);
    }
}
