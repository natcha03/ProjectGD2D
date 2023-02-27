using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class FinUC : MonoBehaviour
{
    public Button backButton;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        backButton = root.Q<Button>("back2");

        backButton.clicked += backButtonPressed;
    }

    void backButtonPressed() { SceneManager.LoadScene("MainMenu"); }
    
}
