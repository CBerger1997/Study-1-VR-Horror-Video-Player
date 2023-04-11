using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterviewScreenController : MonoBehaviour {

    public ViewController viewController;

    public List<GameObject> ScaleRows = new List<GameObject> ();
    public Button ContinueButton;

    List<List<Button>> ScaleButtonList = new List<List<Button>> ();

    List<bool> isScaleRowSelected = new List<bool> ();

    int screenIndex = 0;

    private void Start () {
        int counter = 0;

        foreach ( GameObject row in ScaleRows ) {
            List<Button> buttonList = new List<Button> ();

            foreach ( Button button in row.GetComponentsInChildren<Button> () ) {
                int copy = counter;

                button.name = counter.ToString ();
                button.onClick.AddListener ( delegate { ScaleButtonClicked ( copy ); } );
                counter++;

                buttonList.Add ( button );
            }

            ScaleButtonList.Add ( buttonList );

            bool isRowSelected = false;
            isScaleRowSelected.Add ( isRowSelected );
        }

        ContinueButton.onClick.AddListener ( OnContinueButtonClicked );
        ContinueButton.interactable = false;
    }

    private void Update () {
        bool allRowsSelected = false;

        foreach ( bool rowBool in isScaleRowSelected ) {

            if ( rowBool ) {
                allRowsSelected = true;
            } else {
                allRowsSelected = false;
            }
        }

        if ( allRowsSelected ) {
            ContinueButton.interactable = true;
        }
    }

    public void OnScreenShow () {
        ContinueButton.interactable = false;

        foreach ( GameObject row in ScaleRows ) {
            foreach ( Button button in row.GetComponentsInChildren<Button> () ) {
                button.GetComponent<Image> ().color = Color.white;
            }
        }
    }

    private void ScaleButtonClicked ( int buttonVal ) {


        int listIndex = 0;
        int buttonIndex = 0;

        if ( buttonVal < 5 ) {
            listIndex = 0;
        } else if ( buttonVal < 10 ) {
            listIndex = 1;
            buttonIndex = 5;
        } else if ( buttonVal < 15 ) {
            listIndex = 2;
            buttonIndex = 10;
        }


        foreach ( Button button in ScaleButtonList[ listIndex ] ) {
            Color buttonColour = button.GetComponent<Image> ().color;
            if ( buttonColour == Color.green && buttonIndex != buttonVal ) {
                buttonColour = Color.white;
            } else if ( buttonIndex == buttonVal ) {
                buttonColour = Color.green;
                isScaleRowSelected[ listIndex ] = true;
            }

            button.GetComponent<Image> ().color = buttonColour;

            buttonIndex++;
        }
    }

    private void OnContinueButtonClicked () {
        viewController.MoveToNextInterviewScreen ();
        this.gameObject.SetActive ( false );
    }
}
