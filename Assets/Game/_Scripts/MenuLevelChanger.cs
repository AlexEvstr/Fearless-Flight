using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuLevelChanger : MonoBehaviour
{
    public TMP_Text levelText;
    private int _levelIndex;
    private int _bestLevel;
    [SerializeField] private Button _prevLevelBtn;
    [SerializeField] private Button _nextLevelBtn;

    private void Start()
    {
        _prevLevelBtn.onClick.AddListener(ChoosePrevLevel);
        _nextLevelBtn.onClick.AddListener(ChooseNextLevel);

        _levelIndex = PlayerPrefs.GetInt("LevelIndex", 0);
        _bestLevel = PlayerPrefs.GetInt("BestLevel", 0);
        UpdateLevelWithText();
    }

    public void ChooseNextLevel()
    {
        _levelIndex++;
        PlayerPrefs.SetInt("LevelIndex", _levelIndex);
        UpdateLevelWithText();
    }

    public void ChoosePrevLevel()
    {
        _levelIndex--;
        PlayerPrefs.SetInt("LevelIndex", _levelIndex);
        UpdateLevelWithText();
    }


    private void UpdateLevelWithText()  
    {
        
        if (_levelIndex + 1 < 10)
        {
            levelText.text = $"00{_levelIndex + 1}";
        }
        else if (_levelIndex + 1 >= 10 && _levelIndex + 1 < 100)
        {
            levelText.text = $"0{_levelIndex + 1}";
        }
        else
        {
            levelText.text = $"{_levelIndex + 1}";
        }
    }

    private void Update()
    {
        if (_bestLevel > _levelIndex)
        {
            _nextLevelBtn.interactable = true;

        }
        else _nextLevelBtn.interactable = false;

        if (_levelIndex > 0) _prevLevelBtn.interactable = true;
        else _prevLevelBtn.interactable = false;
    }

}
