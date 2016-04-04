using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


 public class HealthBar : MonoBehaviour
{
    public Player Player;
    public Transform ForgroundSprite;
    public SpriteRenderer ForgroundRenderer;
    public Color MaxHealthColor = new Color(225 / 225f, 63 / 225f, 63 / 225f);
    public Color MinHealthColor = new Color(64 / 255f, 137 / 255f, 255 / 255f);

    public void Update()
    {
        var healthPercent = Player.Health / (float) Player.MaxHealth;

        ForgroundSprite.localScale = new Vector3(healthPercent, 1, 1);

        ForgroundRenderer.color = Color.Lerp(MaxHealthColor, MinHealthColor, healthPercent);

    }
}

