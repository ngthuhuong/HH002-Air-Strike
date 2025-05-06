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

public enum ResourceType
{
    Coin,
    Gem,
    Star
}