using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    public Player m_Player;
    public GameObject m_Wheels;
    public GameObject m_Razor;
    public GameObject m_Pencil;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addWheels()
    {
        m_Player.addSpeed(10f);

        m_Wheels.SetActive(true);
    }

    public void addRazor()
    {
        m_Player.addAttackPower(1);

        Vector3 razorPos = new Vector3(Random.Range(0.1f, 0.3f),Random.Range(0.1f, 1f),Random.Range(0.1f, 0.4f));
        Quaternion razorRot = new Quaternion(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f), 0);
        
        GameObject razor = Instantiate(m_Razor, m_Player.transform,false);
        razor.transform.position = razorPos +m_Player.transform.position;
        razor.transform.rotation = razorRot;
       // razor.transform.parent = m_Player.gameObject.transform;
    }

    public void addPencil()
    {
        m_Player.addAttackPower(2);
        m_Player.addSpeed(-3);
        Quaternion pencilRot = new Quaternion(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f), 0);

        GameObject pencil = Instantiate(m_Pencil, m_Player.transform, false);
        pencil.transform.rotation = pencilRot;
    }
}
