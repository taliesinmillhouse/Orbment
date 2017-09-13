﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillStreakManager : MonoBehaviour
{
    public Text m_display;
    public Text m_descriptionDisplay;

    public string[] m_descriptions;
    public Color[] m_colours;

    public int m_killStreak = 0;
    public float m_timeAllowedBetweenKills = 1.0f;
    private float m_timeOfLastKill = 0.0f;

    private bool m_bLifesteal = false;
    public bool Lifesteal { get; set; }

    public static KillStreakManager m_killStreakManager;

    private void Awake()
    {
        if (m_killStreakManager == null)
        {
            m_killStreakManager = this;
        }
        else if (m_killStreakManager != this)
        {
            Destroy(gameObject);
        }
    }

    public void ResetKillStreak()
    {
        m_killStreak = 0;
        m_timeOfLastKill = 0.0f;
    }

    public void AddKill()
    {
        if(CheckKill())
        {
            m_killStreak++;
            m_timeOfLastKill = Time.time;

            if (m_killStreak >= 5)
            {
                Player.m_Player.m_currHealth += (Player.m_Player.m_maxHealth * 0.10f);
//                Debug.Log(Player.m_Player.m_currHealth);
            }

            if (m_killStreak >= 10)
            {
                Player.m_Player.m_bGodModeIsActive = true;
                Player.m_Player.GodModeTimer = 5.0f;
            }
        }



    }

    public bool CheckKill()
    {
        return (m_timeOfLastKill == 0.0f || Time.time - m_timeOfLastKill <= m_timeAllowedBetweenKills);
    }

    private void Update()
    {
        if(m_display != null)
        {
            m_display.text = "x" + m_killStreak.ToString();
        }

        if(m_descriptionDisplay != null && m_killStreak < m_descriptions.Length)
        {
            m_descriptionDisplay.text = m_descriptions[m_killStreak];
            if(m_killStreak < m_colours.Length)
            {
                m_display.color = m_colours[m_killStreak];
                m_descriptionDisplay.color = m_colours[m_killStreak];
            }
        }

        if(!CheckKill())
        {
            ResetKillStreak();
        }
    }
}
