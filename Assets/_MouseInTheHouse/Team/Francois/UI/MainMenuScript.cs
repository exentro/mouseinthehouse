using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    #region Members
    [SerializeField] private UIScript m_ui;
    #endregion

    #region Public Methods
    public void MainMenu_Credits()
    {
        m_ui.MainMenu_Credits();
    }
    public void MainMenu_Resume()
    {
        m_ui.MainMenu_Resume();
    }
    public void MainMenu_Quit()
    {
        m_ui.MainMenu_Quit();
    }
    public void MainMenu_StartNewGame()
    {
        m_ui.MainMenu_StartNewGame();
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
    #endregion
}