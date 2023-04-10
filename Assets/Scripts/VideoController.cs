using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour {

    VideoPlayer videoPlayer;

    public RenderTexture videoTexture;
    public Material blankSkyBoxMat;
    public Material videoSkyBoxMat;

    public List<VideoClip> videos;

    bool isVideoPlaying;
    bool isInterviewRunning;

    int videoCounter;

    void Start () {
        videoPlayer = GetComponent<VideoPlayer> ();
        videoCounter = 0;
        ShowBlankDot ();

        isVideoPlaying = false;
        isInterviewRunning = false;
    }

    void Update () {
        if ( Input.GetKeyDown ( KeyCode.Space ) ) {
            if ( !isVideoPlaying && !isInterviewRunning ) {
                PlayNextVideo ();
            }
        }

        if ( !videoPlayer.isPlaying && isVideoPlaying == true ) {
            isVideoPlaying = false;
            ShowInterviewStage ();
        }
    }

    private void PlayNextVideo () {        
        isVideoPlaying = true;

        RenderSettings.skybox = videoSkyBoxMat;
        videoPlayer.clip = videos[ videoCounter ];
        videoPlayer.Play ();

        videoCounter++;
    }

    private void ShowInterviewStage () {
        isInterviewRunning = true;
    }

    private void ShowBlankDot () {
        RenderSettings.skybox = blankSkyBoxMat;
    }
}
