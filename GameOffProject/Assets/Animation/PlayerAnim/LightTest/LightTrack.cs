using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.877f, 0.556f, 0.223f)]
[TrackClipType(typeof(LightClip))]
public class LightTrack : TrackAsset
{
    // MIXER
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return base.CreateTrackMixer(graph, go, inputCount);
    }


}
