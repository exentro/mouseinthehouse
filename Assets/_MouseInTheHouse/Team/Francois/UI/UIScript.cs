using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    #region Members
    [SerializeField] private FadeScreenTransitionScript m_blackScreen;
    [SerializeField] private MainMenuScript m_mainMenu;
    [SerializeField] private CreditsScript m_credits;

    private float m_gameTimeScale;
    #endregion

    #region System
    private void Start()
    {
        SetPause(true);
    }
    #endregion

    #region Class Methods
    public void MainMenu_Credits()
    {
        DisablePanels();
        m_blackScreen.StartTransition(PanelCreditsSetActive);
    }
    public void MainMenu_Resume()
    {
        DisablePanels();
        SetPause(false);
    }
    public void MainMenu_Quit()
    {
        DisablePanels();
    }
    public void MainMenu_StartNewGame()
    {
        DisablePanels();
        SetPause(false);
    }
    public void Credits_To_MainMenu()
    {
        DisablePanels();
        m_blackScreen.StartTransition(PanelMainMenuSetActive);
    }
    public void Game_To_MainMenu()
    {
        SetPause(true);
    }
    private void DisablePanels()
    {
        m_mainMenu.SetActive(false);
        m_credits.SetActive(false);
    }
    
    private void SetPause(bool value)
    {
        if(value)
        {
            m_gameTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = m_gameTimeScale;
        }
    }
    private void PanelCreditsSetActive()
    {
        m_credits.SetActive(true);
    }
    private void PanelMainMenuSetActive()
    {
        m_mainMenu.SetActive(true);
    }
    #endregion
}