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
    [SerializeField] private AudioScript m_audio;
    [SerializeField] private MenuManager m_menuManager;
    [SerializeField] private GameObject m_background;
    [SerializeField] private GameObject m_catEyes;
    [SerializeField] private Camera2DFollow m_cameraFollow;

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
        m_cameraFollow.EndGame();
        StartCoroutine(CatEyesCoroutine());
    }
    IEnumerator CatEyesCoroutine()
    {
        yield return new WaitForSeconds(m_timeBetweenTransitionAndCatEyes + m_transitionTime);
        m_audio.m_audioSourceMusic.mute = true;
        m_audio.m_audioSourceVinyl.Play();
        yield return new WaitForSeconds(m_audio.m_audioSourceVinyl.clip.length + 0.5f);
        m_audio.m_audioSourceCat.Play();
        m_catEyes.SetActive(true);
        m_background.SetActive(false);
        yield return new WaitForSeconds(m_timeBetweenCatEyesAndCredits);
        m_menuManager.Credit();
    }
}