using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSniffSniffAnimationScript : MonoBehaviour
{
    [SerializeField] private LayerMask m_PlayersLayer;
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
                    m_mouse.MouseAnimator.SetTrigger(m_mouse.AnimatorParameterMapper.SniffSniff);
                    StartCoroutine(MakeBubbleVisibleAndDisapear(m_mouse));
                }
            }
        }
    }
    IEnumerator MakeBubbleVisibleAndDisapear(MousePlayer mouse)
    {
        mouse.BubbleStartAnimator.gameObject.SetActive(true);
        mouse.BubbleStartAnimator.SetBool("Visible", true);
        yield return new WaitForSeconds(3f);
        mouse.BubbleStartAnimator.SetBool("Visible", false);
        yield return new WaitForSeconds(1f);
        mouse.BubbleStartAnimator.gameObject.SetActive(false);
    }
}