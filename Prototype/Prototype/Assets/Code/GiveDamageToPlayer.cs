using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class GiveDamageToPlayer : MonoBehaviour
{
    public int DamageToGive = 10;
    public int XValueScaler = 5;
    public int MinimumXValue = 10;
    public int MaximumXValue = 20;
    public int YValueScaler = 5;
    public int MinimumYValue = 3;
    public int MaximumYValue = 30;


    private Vector2
        _lastPosition,
        _Velocity;

    public void LateUpdate()
    {
        _Velocity = (_lastPosition - (Vector2)transform.position / Time.deltaTime);
        _lastPosition = transform.position;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        var Player = other.GetComponent<Player>();
        if (Player == null)
            return;

        Player.TakeDamage(DamageToGive, gameObject);
        var controller = Player.GetComponent<CharacterController2D>();
        var totalVelocity = controller.Velocity + _Velocity;

        controller.SetForce(new Vector2(
            -1 * Mathf.Sign(totalVelocity.x) * Mathf.Clamp(Mathf.Abs(totalVelocity.x) * XValueScaler, MinimumXValue, MaximumXValue),
            -1 * Mathf.Sign(totalVelocity.y) * Mathf.Clamp(Mathf.Abs(totalVelocity.y) * YValueScaler, MinimumYValue, MaximumYValue)));
    }
}
