using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitsSlider : MonoBehaviour
{
    public Slider bar;
    public Image hitsFill;

    // Update is called once per frame
    public void Update()
    {
        if (this.bar.value < State.hits) {
            this.bar.value += 0.1f;
        }

        if (this.bar.value >= 100f) {
            Debug.Log(this.bar.value);
            this.hitsFill.color = new Color(0.2f, 0.2f, 0.2f, 1f);
        }
    }
}
