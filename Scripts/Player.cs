using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    private TMP_Text m_pointsText;
    public int m_points = 0;
    private Animator m_ani;
    private ApplicationManager manager;
    private AudioSource m_audio;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("ApplicationManager").GetComponent<ApplicationManager>();

        Component[] audioSources = GetComponentsInChildren<AudioSource>();
        if(audioSources.Length > 0)
        {
            m_audio = (AudioSource)audioSources[0];
        }

        Component[] textFields = GetComponentsInChildren<TMP_Text>();
        if(textFields.Length > 0)
        {
            for(int i = 0; i < textFields.Length; i++)
            {
                if (textFields[i].name != "Selected")
                    m_pointsText = (TMP_Text)textFields[i];
            }
        }

        Component[] animations = GetComponentsInChildren<Animator>();
        if (animations.Length > 0)
        {
            m_ani = (Animator)animations[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Point")
        {
            m_ani.SetTrigger("Jump");
            m_audio.Play();
            m_points++;
            m_pointsText.text = m_points.ToString();
            Destroy(collision.gameObject);            
            manager.AddPointForPlayer(this.name, m_points);
        }
    }
}
