﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private MenuElement[] m_menuElements;
    [SerializeField] private FadeScreenTransitionScript m_fadeTransition;
    [SerializeField] private Camera2DFollow m_CameraFollow;

    private int currentIndex = 0;

    private bool m_menuIsActive = true;
    public bool MenuIsActive
    {
        get { return m_menuIsActive; }
        set
        {
            m_menuIsActive = value;
            if (m_menuElements.Length > 0)
            {
                m_menuElements[currentIndex].Glow(value);
            }
            if (value) { m_CameraFollow.MenuOn(); print("menuon"); }
            else { m_CameraFollow.MenuOff(); }
        }
    }

    public void Next()
    {
        if(m_menuElements.Length > 0)
        {
            m_menuElements[currentIndex].Glow(false);
            currentIndex = (currentIndex + 1) % m_menuElements.Length;
            m_menuElements[currentIndex].Glow(true);
        }
    }

    public void Previous()
    {
        if (m_menuElements.Length > 0)
        {
            m_menuElements[currentIndex].Glow(false);
            if (currentIndex > 0)
            {
                currentIndex--;
            }
            else
            {
                currentIndex = m_menuElements.Length - 1;
            }
            m_menuElements[currentIndex].Glow(true);
        }
    }

    public void Execute()
    {
        if (m_menuElements.Length > 0)
        {
            m_menuElements[currentIndex].Execute();
        }
    }

    public void Credit()
    {
        Debug.Log("Credit");
        m_fadeTransition.StartTransition(CreditEnter);
    }

    public void NewGame()
    {
        MenuIsActive = false;
        Debug.Log("NewGame");
    }

    public void Resume()
    {
        Debug.Log("Resume");
        MenuIsActive = false;
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    [SerializeField] public GameObject PanelCredit;
    private void CreditEnter()
    {
        PanelCredit.SetActive(true);
    }
    public void CreditLeave()
    {
        PanelCredit.SetActive(false);
    }

    //private void Update()
    //{
    //    if (Input.GetButtonDown("Fire1"))
    //    {
    //        Credit();
    //    }
    //    if (Input.GetButtonDown("Fire2"))
    //    {
    //        CreditLeave();
    //    }
    //}
}
