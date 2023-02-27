using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class BeforeUC : MonoBehaviour
{
    public Button yesButton;
    public Button nopeButton;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        yesButton = root.Q<Button>("yes");
        nopeButton = root.Q<Button>("nope");

        nopeButton.clicked += nopeButtonPressed;
        yesButton.clicked += yesButtonPressed;

    }

    void nopeButtonPressed() { SceneManager.LoadScene("MainMenu"); }
    void yesButtonPressed() { SceneManager.LoadScene("level1"); }

}
