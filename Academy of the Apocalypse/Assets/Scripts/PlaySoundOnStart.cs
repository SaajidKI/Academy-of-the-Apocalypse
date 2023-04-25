using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
        public float prefVolume = 1;
    [SerializeField] private AudioClip _clip;

    void Start()
    {
       
        SoundManager.Instance.PlaySound(_clip);
         AudioListener.volume = prefVolume;
        
    }

}
