using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip shootClip;
    public AudioClip sheepHitClip;
    public AudioClip sheepDroppedClip;

    private Vector3 camPosition;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        camPosition = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void playSound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, camPosition);
    }

    public void playShootSound()
    {
        playSound(shootClip);
    }

    public void playSheepHitSound()
    {
        playSound(sheepHitClip);
    }

    public void playSheepDroppedSound()
    {
        playSound(sheepDroppedClip);
    }
}
