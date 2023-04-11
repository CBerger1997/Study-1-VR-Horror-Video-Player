using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour {

    public VideoPlayer videoPlayer { get; set; }
    public List<VideoClip> videos;

    void Start () {
        videoPlayer = GetComponent<VideoPlayer> ();
    }

    public void PlayNextVideo ( int index ) {
        videoPlayer.clip = videos[ index ];
        videoPlayer.Play ();
    }
}
