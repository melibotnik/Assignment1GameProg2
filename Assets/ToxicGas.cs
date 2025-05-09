using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ToxicGas : MonoBehaviour
{
    [SerializeField] private float riseSpeed = 1f;

    void Update()
    {
        transform.Translate(Vector2.up * riseSpeed * Time.deltaTime);
        riseSpeed += Time.deltaTime * 0.05f;
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Gas caught the player!");
            GameManager.Instance.EndGame(false); // Game over!
        }
    }
}

        
    
