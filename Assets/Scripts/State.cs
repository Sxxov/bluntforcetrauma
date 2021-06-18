using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public static int hits = 0;
    public static int drunkeness = 0;

    public void Start() {
        State.hits = 0;
        State.drunkeness = 0;
    }
}
