using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
	AudioManager audioManager;
    Player m_playerRef;
    Mana m_playerMana;
    private Rigidbody m_rigidBody;
    public bool m_healthCap;
    public bool m_manaCap;

    public enum CollectableType
    {
        YellowOrb,
        GreenOrb,
        BlueOrb,
    }

    public CollectableType m_type;

    public int m_healAmount = 10;
    public int m_manaAmount = 100;


    // Use this for initialization
    void Start()
    {
        m_healthCap = true;
        m_manaCap = true;
		audioManager = GameObject.Find ("AudioManager").GetComponent<AudioManager>();
        m_playerRef = GameObject.FindObjectOfType<Player>();
        if(Player.m_Player != null)
        {
            m_playerMana = Player.m_Player.GetComponent<Mana>();
        }
        m_rigidBody = this.GetComponent<Rigidbody>();

        //Physics.IgnoreCollision(GetComponent<Collider>(), m_playerRef.GetComponent<Collider>());
    }

    // Update is called once per frame
    void Update()
    {
        if ((Player.m_Player.m_currHealth + m_healAmount) > Player.m_Player.m_maxHealth)
        {
            m_healthCap = true;
        }
        else
        {
            m_healthCap = false;
        }
        if ((m_playerMana.m_currentMana + m_manaAmount) > m_playerMana.m_maxMana)
        {
            m_manaCap = true;
        }
        else
        {
            m_manaCap = false;
        }

        if (m_type == CollectableType.GreenOrb && Player.m_Player != null)
        {
            if(m_healthCap)
                Physics.IgnoreCollision(GetComponent<Collider>(), Player.m_Player.GetComponent<Collider>(), true);
            else
                Physics.IgnoreCollision(GetComponent<Collider>(), Player.m_Player.GetComponent<Collider>(), false);
        }

        if (m_type == CollectableType.BlueOrb && Player.m_Player != null)
        {
            if(m_manaCap)
                Physics.IgnoreCollision(GetComponent<Collider>(), Player.m_Player.GetComponent<Collider>(), true);
            else
                Physics.IgnoreCollision(GetComponent<Collider>(), Player.m_Player.GetComponent<Collider>(), false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            switch (m_type)
            {
                case CollectableType.YellowOrb:
                    {
                        if(Player.m_Player)
                        {
                            //GameObject.Find ("OrbCollected").GetComponent<Animator> ().SetTrigger ("orbCollected");
                            Player.m_Player.m_orbsCollected++;
							audioManager.OrbPickUp();
                            this.gameObject.SetActive(false);
                        }
                        break;
                    }

                case CollectableType.GreenOrb:
                    {
                        if (Player.m_Player)
                        {
                            if (m_healthCap)
                            {
                                // Do not pick up orb, do not pass go, do not collect $200
                            }
                            else
                            {
                                Player.m_Player.m_currHealth += m_healAmount;
                                audioManager.OrbPickUp();
                                this.gameObject.SetActive(false);
                            }
                        }
                        break;
                    }

                case CollectableType.BlueOrb:
                    {
                        if(m_manaCap)
                        {
                            // Do not pick up orb, do not pass go, do not collect $200
                        }
                        else
                        {
                            m_playerMana.m_currentMana += m_manaAmount;
                            audioManager.OrbPickUp();
                            this.gameObject.SetActive(false);
                        }
                        break;
                    }

                default:
                    {
                        break;
                    }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "OrbBoundary")
        {
            m_rigidBody.velocity = -m_rigidBody.velocity;
            Debug.Log("Exit Bounds");
        }
    }
}
