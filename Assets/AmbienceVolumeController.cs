using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceVolumeController : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    public void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Update()
    {
        this.audioSource.volume = 0.002f * State.drunkeness;
    }
}
