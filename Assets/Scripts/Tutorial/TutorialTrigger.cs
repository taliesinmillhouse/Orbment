﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    private float m_fPopSpeed = 800.0f;
    private float m_fPopDiatanceBuffer = 2.0f;
    private float m_fPoppedInHeight = 200.0f;
    private float m_fPoppedOutHeight = -150.0f;

    private bool m_bPopIn = false;

    public GameObject m_MessageHolder;
	
	void Update()
    {
		if (m_bPopIn)
        {
            PopIn();
        }
        else
        {
            PopOut();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_bPopIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_bPopIn = false;
        }
    }

    private void PopIn()
    {
        if (m_MessageHolder.transform.position.y < m_fPoppedInHeight)
        {
            m_MessageHolder.transform.position = new Vector3(m_MessageHolder.transform.position.x,
                                                             m_MessageHolder.transform.position.y + m_fPopSpeed * Time.deltaTime,
                                                             m_MessageHolder.transform.position.z);
        }
    }

    private void PopOut()
    {
        if (m_MessageHolder.transform.position.y > m_fPoppedOutHeight)
        {
            m_MessageHolder.transform.position = new Vector3(m_MessageHolder.transform.position.x,
                                                             m_MessageHolder.transform.position.y - m_fPopSpeed * Time.deltaTime,
                                                             m_MessageHolder.transform.position.z);
        }
    }
}