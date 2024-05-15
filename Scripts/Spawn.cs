using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    [Header("Prefab to spawn")]
    public Object prefab;
    [Header("Number of objects to spawn")]
    public int count = 100;
    [Header("Bounciness (0-100%)")]
    public int bounciness = 50;

    private IEnumerator coroutine;
    private PhysicsMaterial2D pm;
    private int cntObject;

    private bool WaitForDestroy = false;

    private ApplicationManager manager;

    // Start is called before the first frame update
    void Start()
    {
        pm = new PhysicsMaterial2D();
        pm.bounciness = (bounciness/100f);

        manager = GameObject.Find("ApplicationManager").GetComponent<ApplicationManager>();
        manager.SetMaxPoints(count);

        StartSpawn();
    }

    public void StartSpawn()
    {
        coroutine = SpawnObjects();
        StartCoroutine(coroutine);
    }

    IEnumerator SpawnObjects()
    {
        while(cntObject < count)
        {
            float seconds = Random.Range(0.5f, 4);

            int newObjects = Random.Range(0, 11);
            if(cntObject + newObjects > count)
            {
                newObjects = count- cntObject;
            }
            if(newObjects > 0)
            {
                for(int i = 0; i < newObjects; i++)
                {
                    SpawnNewObject();
                    cntObject++;
                }
            }
            yield return new WaitForSeconds(seconds);
        }
        WaitForDestroy = true;
    }

    private void SpawnNewObject()
    {
        GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        // Modify the clone to your heart's content

        clone.transform.SetParent(GameObject.Find("Spawn").transform);
        clone.GetComponent<Collider2D>().sharedMaterial = pm;

        clone.transform.localScale = new Vector3(1f, 1f, 1f);

        float x = (GameObject.Find("Spawn").GetComponent<RectTransform>().sizeDelta.x / 2) -20;
        x = Random.Range(x * -1, x);

       clone.transform.localPosition = new Vector3(x, 0, 0);
    }


    // Update is called once per frame
    void Update()
    {
        if(WaitForDestroy && GameObject.FindGameObjectsWithTag("Point").Length == 0)
        {
            manager.CheckWinner();
        }
        
    }
}
