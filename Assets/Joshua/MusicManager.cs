using System.Collections;
using System.Collections.Generic;
using System.Media;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public enum SoundClipsInts { Default, GoldPickUp, Attack, Hit, Death, Buying };

public class MusicManager : MonoBehaviour
{

    //The AudioSource to which we play any clips
    private AudioSource A_Source;

    //The audioclips which you should assign through inspector
    public AudioClip Clip_00;
    public AudioClip Clip_01;
    public AudioClip Clip_02;
    public AudioClip Clip_03;
    public AudioClip Clip_04;
    public AudioClip Clip_05;

    //Singleton accessor
    public static MusicManager Instance;

    void Awake()
    { Instance = this; }

    void Start()
    {
        //Add the audio source
        A_Source = gameObject.AddComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySoundTrack(SoundClipsInts TrackID)
    {

        //Stop any playing music
        A_Source.Stop();

        switch (TrackID)
        {
            case SoundClipsInts.GoldPickUp:
                A_Source.PlayOneShot(Clip_01);
                break;

            case SoundClipsInts.Attack:
                A_Source.PlayOneShot(Clip_02);
                break;

            case SoundClipsInts.Hit:
                A_Source.PlayOneShot(Clip_03);
                break;

            case SoundClipsInts.Death:
                A_Source.PlayOneShot(Clip_04);
                break;

            case SoundClipsInts.Buying:
                A_Source.PlayOneShot(Clip_05);
                break;

            default:
                A_Source.PlayOneShot(Clip_00);
                break;
        }

    }


}
