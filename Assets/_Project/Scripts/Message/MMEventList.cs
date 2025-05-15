public struct EGameOver
{
    
}

/// <summary>
/// When player earn score
/// </summary>
public struct EEarnScore
{
    
}

/// <summary>
/// When an enemy die
/// </summary>
public struct EEnemyDie
{
    
}

/// <summary>
/// When data changed
/// </summary>
public struct EDataChanged
{
    
}

public struct EGameStart
{
    
}

public struct EActiveBooster
{
    public BoosterType BoosterType;
    public float Duration;

    public EActiveBooster(BoosterType boosterType, float duration)
    {
        this.BoosterType = boosterType;
        this.Duration = duration;
    }
    
}
public struct EEarnResource
{
    public ResourceType resourceType;
    public int amount;
    
    public EEarnResource(ResourceType resourceType, int amount)
    {
        this.resourceType = resourceType;
        this.amount = amount;
    }
}

public struct EAchievementUnlocked
{
    public AchievementType achievementType;
    public string achievementName;
    
    public EAchievementUnlocked(AchievementType achievementType, string achievementName)
    {
        this.achievementType = achievementType;
        this.achievementName = achievementName;
    }
}

public enum ResourceType
{
    Coin,
    Gem,
    Star
}

public enum BoosterType
{
    Speed,
    Shield,
}