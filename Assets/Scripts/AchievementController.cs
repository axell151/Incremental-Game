using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementController : MonoBehaviour
{
    public GameObject Coin;
    public GameObject Diamond;
    private static AchievementController _instance = null;

    public static AchievementController Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<AchievementController>();
            }
            return _instance;
        }
    }
    [SerializeField] private Transform _popupTransform;
    [SerializeField] private Text _popUpText;
    [SerializeField] private float _popUpShowDuration = 2.5f;
    [SerializeField] private List<AchievementData> _achievementList;

    private float _popUpShowDurationCounter;

    private void Update()
    {
        if(_popUpShowDurationCounter > 0)
        {
            _popUpShowDurationCounter -= Time.unscaledDeltaTime;
            _popupTransform.localScale = Vector3.LerpUnclamped(_popupTransform.localScale, Vector3.one, 0.5f);
        }
    }

    public void UnlockAchievement(AchievementType type, string value)
    {
        AchievementData achievement = _achievementList.Find(a => a.Type == type && a.Value == value);
        if(achievement != null && !achievement.IsUnlocked)
        {
            achievement.IsUnlocked = true;
            ShowAchievementPopUp(achievement);
        }
    }

    private void ShowAchievementPopUp(AchievementData achievement)
    {
        _popUpText.text = achievement.Title;
        _popUpShowDurationCounter = _popUpShowDuration;
        _popupTransform.localScale = Vector2.right;
        if(achievement.Title == "Unlock Diamond Skin")
        {
            Coin.gameObject.SetActive(false);
            Diamond.gameObject.SetActive(true);
        }
    }
}

[System.Serializable]
public class AchievementData
{
    public string Title;
    public AchievementType Type;
    public string Value;
    public bool IsUnlocked;
}

public enum AchievementType
{
    UnlockResource
}