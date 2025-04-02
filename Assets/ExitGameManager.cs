using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGameManager : MonoBehaviour
{
    [Header("menu refs")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject confirmationPanel;

    [Header("button refs")]
    [SerializeField] private Button exitButton;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    // Start is called before the first frame update
    void Start()
    {
        confirmationPanel.SetActive(false);

        exitButton.onClick.AddListener(ShowConfirmationPanel);
        yesButton.onClick.AddListener(ExitGame);
        noButton.onClick.AddListener(HideConfirmationPanel);
    }

    public void ShowConfirmationPanel()
    {
        mainMenu.SetActive(false);
        confirmationPanel.SetActive(true);
    }

    public void HideConfirmationPanel()
    {
        confirmationPanel.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        // Jeœli jesteœmy w edytorze Unity, zatrzymaj tryb play
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // W zbudowanej aplikacji, rzeczywiœcie zamknij aplikacjê
        Application.Quit();
#endif
        Debug.Log("Wyjœcie z gry");
    }
}
