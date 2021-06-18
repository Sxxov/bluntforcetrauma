using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public GameObject target;
    public void OnCollisionEnter(Collision collision) {
        GameObject gameObject = collision.gameObject;

        if (gameObject.Equals(this.target)) {
            ++State.hits;
            ++State.drunkeness;
        }
    }
}
