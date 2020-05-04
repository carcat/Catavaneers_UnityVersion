using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickUp : MonoBehaviour
{
    public SoundClipsInts soundCue = SoundClipsInts.GoldPickUp;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerInventory>().gold += 10;
            MusicManager.Instance.PlaySoundTrack(soundCue);
        }
    }

}
