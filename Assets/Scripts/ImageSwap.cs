using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwap : MonoBehaviour
{
    public enum TYPE
    {
        SFX,
        BGM
    }
    public Sprite[] Source;     // 0 = off, 1 = on;
    public Image image;
    public TYPE buttonType;

    private void Update()
    {
        if (AudioManager.Instance.EffectsSource.mute && buttonType == TYPE.SFX)
            image.sprite = Source[1];
        else if (!AudioManager.Instance.EffectsSource.mute && buttonType == TYPE.SFX)
            image.sprite = Source[0];
        if (AudioManager.Instance.MusicSource[0].mute && buttonType == TYPE.BGM)
            image.sprite = Source[1];
        else if (!AudioManager.Instance.MusicSource[0].mute && buttonType == TYPE.BGM)
            image.sprite = Source[0];
    }
}
