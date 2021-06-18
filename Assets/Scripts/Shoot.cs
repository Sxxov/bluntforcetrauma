using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Component bullet;
    public Component playerCamera;

    public AudioClip[] audioClips;
    
    private AudioSource audioSource;

    // Start is called before the first frame update
    public void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Component instance = Shoot.Instantiate(bullet);
            Rigidbody body = instance.GetComponent<Rigidbody>();

            instance.transform.position = new Vector3(
                this.transform.position.x,
                this.transform.position.y + 0.92f,
                this.transform.position.z
            ) + (this.transform.forward * 2f);

            ++State.hits;
            ++State.drunkeness;

            this.Invoke("OnRecover", 5);

            instance.transform.localScale = Vector3.one * (1f + (State.hits / 10f));

            body.velocity = new Vector3(
                this.transform.forward.x,
                this.playerCamera.transform.forward.y,
                this.transform.forward.z
            ) * 100f;

            this.PlaySound();
        }
    }

    public void OnRecover() {
        if (State.drunkeness > 0) {
            --State.drunkeness;
        }
    }

    private void PlaySound() {
        this.audioSource.clip = this.audioClips[Random.Range(0, this.audioClips.Length)];
        this.audioSource.Play();
    }
}
