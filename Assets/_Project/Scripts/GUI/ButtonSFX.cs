using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSFX : MonoBehaviour, ISelectHandler
{
    [SerializeField] private AudioManager.SoundName  buttonClickSound = AudioManager.SoundName.SFX_Button_Click;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        MMEventManager.TriggerEvent(new EPlaySound(buttonClickSound));
    }
}
