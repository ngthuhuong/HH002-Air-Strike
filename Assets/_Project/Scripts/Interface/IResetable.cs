using UnityEngine;

public interface IResetable
{
    bool isActivated { get; set; }
    void ResetState();
    void StartState();
    void EndState();
}