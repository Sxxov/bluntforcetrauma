using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Drunk : MonoBehaviour
{
    public PostProcessVolume volume;
    // private PostProcessVolume volume;
    private DepthOfField depthOfField;
    private Grain grain;
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;
    private Vignette vignette;
    // Start is called before the first frame update
    public void Start()
    {
        PostProcessProfile profile = this.volume.profile;

        this.depthOfField = profile.AddSettings<DepthOfField>();
        this.depthOfField.enabled.Override(true);
        this.depthOfField.focusDistance.Override(1f);

        this.grain = profile.AddSettings<Grain>();
        this.grain.enabled.Override(true);
        this.grain.colored.Override(false);
        this.grain.size.Override(0.3f);

        this.chromaticAberration = profile.AddSettings<ChromaticAberration>();
        this.chromaticAberration.enabled.Override(true);

        this.lensDistortion = profile.AddSettings<LensDistortion>();
        this.lensDistortion.enabled.Override(true);

        this.vignette = profile.AddSettings<Vignette>();
        this.vignette.enabled.Override(true);
        this.vignette.smoothness.Override(0.684f);

        Debug.Log(profile.settings);

        // this.volume = PostProcessManager.instance.QuickVolume(
        //     gameObject.layer, 
        //     100f, 
        //     this.depthOfField
        // );
    }

    // Update is called once per frame
    public void Update()
    {
        this.depthOfField.aperture.Override(Mathf.Clamp(28f - State.drunkeness, 0, 28));
        this.grain.intensity.Override(0.5f + State.drunkeness / 50f);
        this.chromaticAberration.intensity.Override(0.05f + State.drunkeness / 10f);
        this.lensDistortion.intensity.Override(-27f - State.drunkeness / 2f);
        this.vignette.intensity.Override(0.3f + State.drunkeness / 100f);
    }

    public void OnDestroy()
    {
        // RuntimeUtilities.DestroyVolume(this.volume, true, true);
        Destroy(this.volume.profile);
    }
}
