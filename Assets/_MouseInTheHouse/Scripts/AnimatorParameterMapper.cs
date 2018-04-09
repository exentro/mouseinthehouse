using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorParameterMapper : MonoBehaviour
{
    [SerializeField] private string m_horizontalSpeed = "HorizontalSpeed";
    public string HorizontalSpeed
    {
        get { return m_horizontalSpeed; }
    }

    [SerializeField] private string m_verticalSpeed = "VerticalSpeed";
    public string VerticalSpeed
    {
        get { return m_verticalSpeed; }
    }

    [SerializeField] private string m_horizontalInput = "HorizontalInput";
    public string HorizontalInput
    {
        get { return m_horizontalInput; }
    }

    [SerializeField] private string m_verticalInput = "VerticalInput";
    public string VerticalInput
    {
        get { return m_verticalInput; }
    }

    [SerializeField] private string m_verticalVelocity = "VerticalVelocity";
    public string VerticalVelocity
    {
        get { return m_verticalVelocity; }
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

    [SerializeField] private string m_nibble = "Nibble";
    public string Nibble
    {
        get { return m_nibble; }
    }
}
