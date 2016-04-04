using UnityEngine;
using System.Collections;

public class JumpUnderneath : MonoBehaviour
{
    public Player Player;

    public void Update()
    {
        var player  = Player.transform.position;
        var platform = gameObject.GetComponent<Collider2D>();
        var platformPos = platform.transform.position;

        if (player.y < platformPos.y)
            platform.enabled = false;

        if ((player.y) - 3 > platformPos.y)
        {
            platform.enabled = true;
        }
    }
}