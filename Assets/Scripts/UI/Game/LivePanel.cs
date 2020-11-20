using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class LivePanel : MonoBehaviour
{    
    [SerializeField] private Image[] _hearts;
      
    private void Awake()
    {
        _hearts = GetComponentsInChildren<Image>();
    }   

    public void UpdateUI(int currentHp)
    {       
        Assert.IsTrue(_hearts.Length >= currentHp);

        for (int i = 0; i < _hearts.Length; i++)
        {
            var heart = _hearts[i];
            if (i < currentHp)
            {
                heart.enabled = true;
            }
            else
            {
                heart.enabled = false;
            }
        }
    }
}
