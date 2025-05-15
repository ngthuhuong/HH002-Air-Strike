using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Achievements/Achievement")]
public class Achievement : ScriptableObject
{
    public AchievementType type; // Unique ID for the achievement
    public string title;         // Title of the achievement
    public string description;   // Description of the achievement
    public int targetValue;      // Target value to unlock the achievement
    public Sprite icon;          // Icon for the achievement
    public RewardPoolCoin.RewardType rewardType; 
    public int rewardValue;
    
}