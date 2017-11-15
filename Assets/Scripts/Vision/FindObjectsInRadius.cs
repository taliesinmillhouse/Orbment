using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindObjectsInRadius : MonoBehaviour
{
    //public static FindObjectsInRadius m_foir;

    public string m_targetTag;
    public float m_sightRadius = 20;
    public float m_sightAngle = 45;
    public float m_attackRange = 1.5f;

    public bool inRange = false;
    public bool inSight = false;
    public Vector3 direction;
    //private Vector3 forwardPoint;
    //private Vector3 leftPoint;
    //private Vector3 rightPoint;
    //private Collider[] insideSphere;

    public bool m_manualSetTarget;
    [HideInInspector]
    public Transform m_target;

    string m_sLayer;
    int m_iLayer;

    private void Awake()
    {
        m_sLayer = LayerMask.LayerToName(10);
        m_iLayer = LayerMask.GetMask("Player");

        Debug.Log("GetName = " + m_sLayer);
        Debug.Log("GetLayer = " + m_iLayer);

    }
    // Use this for initialization
    void Start()
    {
        direction = this.transform.forward;
        //m_target = Player.m_Player.transform;
    }

    //maybe turn this into a call so it's not run every frame?
    private void FixedUpdate()
    {
        //insideSphere = Physics.OverlapSphere(this.transform.position, m_sightRadius);

        //if (m_targetTag == "")
        //{
        //    return;
        //}

        //foreach (Collider col in insideSphere)
        //{
            //if (col.CompareTag(m_targetTag))
            //{
                //direction = col.transform.position - this.transform.position;
        direction = Player.m_player.transform.position - this.transform.position;
        direction.y = 0;
        direction.Normalize();

        float angle = Vector3.Angle(this.transform.forward, direction);
        if (angle < m_sightAngle)
        {
            RaycastHit hit;
            // Player Layer mask = 1024
            // wall layer mask = 0 "default"
            
            int layerMask = LayerMask.GetMask("Default", "Player");

            //if (Physics.Raycast(this.transform.position, direction * m_sightRadius, out hit, 20.0f, 1024))
//            if (Physics.Raycast(this.transform.position, direction * m_sightRadius, out hit))
            if (Physics.Raycast(this.transform.position, direction * m_sightRadius, out hit, 20.0f, layerMask, QueryTriggerInteraction.Collide))
            {
                Debug.Log(this.transform.position.ToString());
                Debug.Log(hit.collider.name);

                if (hit.collider.CompareTag(m_targetTag))
                {
                    inSight = true;
                    m_target = hit.collider.transform;
                }
                else
                {
                    inSight = false;
                    m_target = null;
                }
            }
            else
            {
                inSight = false;
                m_target = null;
            }
        }
        else
        {
            inSight = false;
            m_target = null;
        }
            //}
        //}
        
        if(m_target != null)
        {
            float distance = Vector3.Distance(this.transform.position, m_target.position);

            if(distance <= m_attackRange)
            {
                inRange = true;
            }
            else
            {
                inRange = false;
            }
        }
        else
        {
            inRange = false;
        }
    }

    void OnDrawGizmos()
    {
        Vector3 leftLine = Quaternion.Euler(this.transform.up * m_sightAngle) * this.transform.forward;
        Vector3 rightLine = Quaternion.Euler(this.transform.up * -m_sightAngle) * this.transform.forward;
    }
}