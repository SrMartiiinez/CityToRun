using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutedAudio : MonoBehaviour
{

    public void MutedToggle(bool muted)
    {
        if (muted)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }
}
