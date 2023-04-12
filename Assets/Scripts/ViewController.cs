using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ViewController : MonoBehaviour {

    public List<InterviewScreenController> interviewScreenControllers = new List<InterviewScreenController> ();
    public Material CrossSkyBoxMat;
    public Material VideoSkyBoxMat;
    public VideoController videoController;
    public GameObject RightHandController;

    int interviewCount = -1;
    int videoCount = 0;

    float Timer = 0.0f;

    bool isCrossScreenActive = false;
    bool isVideoActive = false;
    bool isTimerRunning = false;

    List<UnityEngine.XR.InputDevice> rightHandDevices = new List<UnityEngine.XR.InputDevice> ();


    void Start () {
        MoveToCrossScreen ();
        Timer = 5.0f;
        isTimerRunning = true;

        foreach ( InterviewScreenController controller in interviewScreenControllers ) {
            controller.gameObject.SetActive ( false );
        }

        UnityEngine.XR.InputDevices.GetDevicesAtXRNode ( UnityEngine.XR.XRNode.RightHand, rightHandDevices );

        Unity.XR.Oculus.Performance.TrySetDisplayRefreshRate ( 90f );
    }

    void Update () {

        if ( Timer > 0.0f ) {
            Timer -= Time.deltaTime;
        } else {
            isTimerRunning = false;
        }

        bool triggerValue;

        //if ( rightHandDevices[ 0 ].TryGetFeatureValue ( UnityEngine.XR.CommonUsages.secondaryButton, out triggerValue ) && triggerValue ) {
        if ( Input.GetKeyDown ( KeyCode.Space ) ) {
            if ( isCrossScreenActive && !isTimerRunning ) {
                MoveToVideoScreen ();
                isCrossScreenActive = false;
                Timer = 5.0f;
                isTimerRunning = true;
            }
        }

        if ( !videoController.videoPlayer.isPlaying && isVideoActive && !isTimerRunning ) {
            isVideoActive = false;
            MoveToNextInterviewScreen ();
        }
    }

    public void MoveToNextInterviewScreen () {
        RightHandController.GetComponent<XRInteractorLineVisual> ().enabled = true;

        if ( interviewCount == 3 ) {
            interviewCount = -1;
            videoCount++;
            MoveToCrossScreen ();
            Timer = 5.0f;
            isTimerRunning = true;
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
        RightHandController.GetComponent<XRInteractorLineVisual> ().enabled = false;
        RenderSettings.skybox = CrossSkyBoxMat;
        isCrossScreenActive = true;
    }
}
