using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public Button startButton;
    public Button exitButton;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("startButton");
        exitButton = root.Q<Button>("exitButton");
        
        startButton.clicked += startButtonPressed;
        exitButton.clicked += exitButtonPressed;
        
    }

    void startButtonPressed() { SceneManager.LoadScene("Before"); }
    void exitButtonPressed() { Application.Quit(); }

}
