using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InstaKill : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        LevelManager.Instance.KillPlayer();
    }
}