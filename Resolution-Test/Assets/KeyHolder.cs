using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    private List<Key.KeyType> keyList;

    private void Awake()
    {
        keyList = new List<Key.KeyType>();
    }
        
    public void AddKey(Key.KeyType keyType )
    {
        Debug.Log("Added Key: " + keyType.ToString());
        keyList.Add(keyType);
    }

    public void RemoveKey(Key.KeyType keyType)
    {
        keyList.Remove(keyType);
    }

    public bool ContainsKey(Key.KeyType keyType)
    {
        return keyList.Contains(keyType);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Key key = collider.GetComponent<Key>();
        if (key != null)
        {
            GameObject thePlayer = GameObject.Find("Player");
            PlayerController playerController = thePlayer.GetComponent<PlayerController>();

            Debug.Log("Candies: " + playerController.candies);

            if (playerController.candies >= key.GetKeyCost())
            {
                playerController.candies = playerController.candies - key.GetKeyCost();
                AddKey(key.GetKeyType());
                Destroy(key.gameObject);

                //Activate Crunchies
                GameObject crunchyenemies = GameObject.Find("Crunchy");
                crunchyenemies.GetComponent<FollowPlayer>().hostile = true;
}
            else
            {
                Debug.Log("Not enough candies! Needed:" + key.GetKeyCost());
            }    
        }

        KeyDoor keyDoor = collider.GetComponent<KeyDoor>();
        if (keyDoor != null)
        {
            Debug.Log("Has key (" + keyDoor.GetKeyType() + "): " + ContainsKey(keyDoor.GetKeyType()));
            if (ContainsKey(keyDoor.GetKeyType()))
            {
                //currently holding key to open door
                Debug.Log("Door Opened: " + keyDoor.GetKeyType().ToString());
                RemoveKey(keyDoor.GetKeyType());
                keyDoor.OpenDoor();
            }
        }

    }
}
