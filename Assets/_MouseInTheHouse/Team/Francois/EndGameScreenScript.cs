using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScreenScript : MonoBehaviour
{
    [SerializeField] private float m_timeBetweenDanceAndTransition = 3f;
    [SerializeField] private float m_transitionTime = 2f;
    [SerializeField] private float m_timeBetweenTransitionAndCatEyes = 3f;
    [SerializeField] private float m_timeBetweenCatEyesAndCredits = 3f;
    [SerializeField] private FadeScreenTransitionScript m_fadeScript;
    [SerializeField] private MenuManager m_menuManager;
    [SerializeField] private GameObject m_background;
    [SerializeField] private GameObject m_catEyes;

    public void StartEndGame()
    {
        StartCoroutine(StartEndGameCoroutine());
    }
    IEnumerator StartEndGameCoroutine()
    {
        yield return new WaitForSeconds(m_timeBetweenDanceAndTransition);
        m_fadeScript.StartTransition(m_transitionTime, SetLastScene, m_transitionTime);
    }
    private void SetLastScene()
    {
        m_catEyes.SetActive(false);
        m_background.SetActive(true);
        //TODO: teleport camera
        StartCoroutine(CatEyesCoroutine());
    }
    IEnumerator CatEyesCoroutine()
    {
        yield return new WaitForSeconds(m_timeBetweenTransitionAndCatEyes + m_transitionTime);
        m_catEyes.SetActive(true);
        m_background.SetActive(false);
        //TODO: sound
        yield return new WaitForSeconds(m_timeBetweenCatEyesAndCredits);
        m_menuManager.Credit();
    }
}