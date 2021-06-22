using UnityEngine;

public static class HexMetrices
{
    public const float m_OuterRadius = 10.0f;
    public const float m_InnerRadius = m_OuterRadius * 0.866025404f;

    public static Vector3[] m_Corners =
    {
        new Vector3(0.0f, 0.0f, m_OuterRadius),
        new Vector3(m_InnerRadius, 0.0f, 0.5f * m_OuterRadius),
        new Vector3(m_InnerRadius, 0.0f, -0.5f * m_OuterRadius),
        new Vector3(0.0f, 0.0f, -m_OuterRadius),
        new Vector3(-m_InnerRadius, 0.0f, -0.5f * m_OuterRadius),
        new Vector3(-m_InnerRadius, 0.0f, 0.5f * m_OuterRadius),
        new Vector3(0.0f, 0.0f, m_OuterRadius)
    };
}
