using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    // This class is a Singleton test
    private static PickupManager instance;
    [SerializeField] GameObject pickupPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static PickupManager GetInstance()
    {
        return instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newPickup = Instantiate(pickupPrefab);
            newPickup.transform.position = new Vector3(Random.Range(0f, 5f), 1f, Random.Range(0f, 5f)); 
        }
    }
}
