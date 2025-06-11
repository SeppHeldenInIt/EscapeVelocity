using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ItemPool : MonoBehaviour
{
    public static ItemPool instance;

    List<GameObject> poolObjects = new List<GameObject>();
    [SerializeField] int amoutToPool;

    [SerializeField] private GameObject itemPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amoutToPool; i++)
        {
            GameObject obj = Instantiate(itemPrefab);
            obj.SetActive(false);
            poolObjects.Add(obj);


        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < poolObjects.Count; i++)
        {
            if (!poolObjects[i].activeInHierarchy)
            {
                return poolObjects[i];
            }
        }

        return null;
    }
}