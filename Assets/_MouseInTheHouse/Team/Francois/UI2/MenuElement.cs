﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuElement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_halo;

    public void Glow(bool value)
    {
        Debug.Log(string.Format("{0} : Glow {1}", gameObject.name, value));
        m_halo.enabled = value;
    }

    [SerializeField] private UnityEvent m_MyEvent;
    public void Execute()
    {
        m_MyEvent.Invoke();
    }
}
