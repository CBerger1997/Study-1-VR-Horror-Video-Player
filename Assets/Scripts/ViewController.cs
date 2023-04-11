using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour {

    public List<InterviewScreenController> interviewScreenControllers = new List<InterviewScreenController> ();
    public Material CrossSkyBoxMat;
    public Material VideoSkyBoxMat;
    public VideoController videoController;

    int interviewCount = -1;
    int videoCount = 0;

    float Timer = 0.0f;

    bool isCrossScreenActive = false;
    bool isVideoActive = false;
    bool isTimerRunning = false;

    void Start () {
        MoveToCrossScreen ();

        foreach ( InterviewScreenController controller in interviewScreenControllers ) {
            controller.gameObject.SetActive ( false );
        }
    }

    void Update () {

        if(Timer > 0.0f) {
            Timer -= Time.deltaTime;
        } else {
            isTimerRunning = false;
        }

        if ( Input.GetKeyDown ( KeyCode.Space ) ) {
            if ( isCrossScreenActive ) {
                MoveToVideoScreen ();
                isCrossScreenActive = false;
                Timer = 5.0f;
                isTimerRunning = true;
            }
        }

        if ( !videoController.videoPlayer.isPlaying && isVideoActive == true && !isTimerRunning) {
            isVideoActive = false;
            MoveToNextInterviewScreen ();
        }
    }

    public void MoveToNextInterviewScreen () {

        if ( interviewCount == 3 ) {
            interviewCount = -1;
            videoCount++;
            MoveToCrossScreen ();
        } else {
            if ( interviewCount == -1 ) {
                if ( videoCount == 0 || videoCount % 2 == 0 ) {
                    interviewCount = 0;
                    Debug.Log ( "Calm" );
                } else {
                    Debug.Log ( "Fear" );
                    interviewCount = 1;
                }
            }

            interviewScreenControllers[ interviewCount ].gameObject.SetActive ( true );
            interviewScreenControllers[ interviewCount ].OnScreenShow ();

            switch ( interviewCount ) {
                case 0:
                    interviewCount = 2;
                    break;
                case 1:
                    interviewCount = 2;
                    break;
                case 2:
                    interviewCount = 3;
                    break;
            }
        }
    }

    public void MoveToVideoScreen () {
        RenderSettings.skybox = VideoSkyBoxMat;
        isVideoActive = true;

        videoController.PlayNextVideo ( videoCount );
    }

    public void MoveToCrossScreen () {
        RenderSettings.skybox = CrossSkyBoxMat;
        isCrossScreenActive = true;
    }
}
