using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrunknessSlider : MonoBehaviour
{
    public Slider bar;

    // Update is called once per frame
    public void Update()
    {
        if (this.bar.value > State.drunkeness) {
            this.bar.value -= 0.1f;
        }

        if (this.bar.value < State.drunkeness) {
            this.bar.value += 0.1f;
        }
    }
}
