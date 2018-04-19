using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartVictoryAnimationScript : MonoBehaviour
{
    [SerializeField] private LayerMask m_PlayersLayer;
    [SerializeField] private EndGameScreenScript m_endGameScript;
    List<int> m_alreadyTriggeredForTheseID;

    private void Start()
    {
        if (m_alreadyTriggeredForTheseID == null) m_alreadyTriggeredForTheseID = new List<int>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_PlayersLayer.Contains(collision.gameObject.layer))
        {
            MousePlayer m_mouse = collision.gameObject.GetComponentInParent<MousePlayer>();
            if (m_mouse != null)
            {
                if (!m_alreadyTriggeredForTheseID.Contains(m_mouse.PlayerID))
                {
                    m_alreadyTriggeredForTheseID.Add(m_mouse.PlayerID);
                    m_mouse.MouseAnimator.SetTrigger(m_mouse.AnimatorParameterMapper.VictoryDance);

                    StartCoroutine(MakeBubbleVisibleAndDisapear(m_mouse));

                    if (m_endGameScript == null) m_endGameScript = FindObjectOfType<EndGameScreenScript>();
                    if (m_endGameScript == null)
                    {
                        Debug.LogError("\"EndGameScreenScript\" is not setted");
                    }
                    else if (m_alreadyTriggeredForTheseID.Count >= 2) m_endGameScript.StartEndGame();
                }
            }
        }
    }
    IEnumerator MakeBubbleVisibleAndDisapear(MousePlayer mouse)
    {
        mouse.BubbleEndAnimator.gameObject.SetActive(true);
        mouse.BubbleEndAnimator.SetBool("Visible", true);
        yield return new WaitForSeconds(5f);
        mouse.BubbleEndAnimator.SetBool("Visible", false);
        mouse.BubbleEndAnimator.gameObject.SetActive(false);
    }
}