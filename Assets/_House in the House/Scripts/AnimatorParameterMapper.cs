using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorParameterMapper : MonoBehaviour
{
    [SerializeField] private string m_horizontal = "HorizontalSpeed";
    public string HorizontalSpeed
    {
        get { return m_horizontal; }
    }

    [SerializeField] private string m_vertical = "VerticalSpeed";
    public string VerticalSpeed
    {
        get { return m_vertical; }
    }

    [SerializeField] private string m_ground = "Ground";
    public string Ground
    {
        get { return m_ground; }
    }

    [SerializeField] private string m_jump = "Jump";
    public string Jump
    {
        get { return m_jump; }
    }

    [SerializeField] private string m_push = "Push";
    public string Push
    {
        get { return m_push; }
    }

    [SerializeField] private string m_climb = "Climb";
    public string Climb
    {
        get { return m_climb; }
    }

    [SerializeField] private string m_crouch = "Crouch";
    public string Crouch
    {
        get { return m_crouch; }
    }
}
