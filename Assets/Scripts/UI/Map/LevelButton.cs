using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{   
    [SerializeField] private LevelDataSettings _levelSettings;   

    public LevelDataSettings LevelSettings => _levelSettings; 

    public event Action OnLoseEvent;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void SetEnable(bool value)
    {
        _button.interactable = value;
    }
}
