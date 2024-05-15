using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    private AudioSource m_audio;

    // Start is called before the first frame update
    void Start()
    {
        Component[] audioSources = GetComponentsInChildren<AudioSource>();
        if (audioSources.Length > 0)
        {
            m_audio = (AudioSource)audioSources[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Point")
        {
            StartCoroutine(DestroyObject(collision.gameObject));
        }
    }

    private IEnumerator DestroyObject(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        if (obj != null)
        {
            Destroy(obj);
            m_audio.Play();
        }
    }
}
