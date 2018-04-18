using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour
{
    #region Members
    [SerializeField] private UIScript m_ui;
    [SerializeField] private Animator m_anim;
    #endregion
    
    #region Class Methods
    public void Credits_To_MainMenu()
    {
        m_ui.Credits_To_MainMenu();
    }
    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
    #endregion
}