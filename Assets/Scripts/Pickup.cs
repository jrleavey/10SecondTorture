using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Player Player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.tag == "Player")
        {

            Destroy(this.gameObject);
        }
    }
}
