using UnityEngine;
using System.Collections;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public float Speed;


    private CharacterController2D _controller;
    private Vector2 _direction;
    private float _wait = 1f;

    public void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        _direction = new Vector2(-1, 0);


    }

    public void Update()
    {
        var rayCast = Physics2D.Raycast(transform.position, _direction, 5, 1 << LayerMask.NameToLayer("player"));

        if ((_direction.x < 0 && _controller.State.IsCollidingLeft) || (_direction.x > 0 && _controller.State.IsCollidingRight))
        {
            _direction = -_direction;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        if (!rayCast)
        {
            _controller.SetHorizontalForce(_direction.x * Speed);
            return;
        }

        _controller.SetHorizontalForce(0f);

        if ((_wait -= Time.deltaTime) > 0)
            return;

        if (_controller.CanJump)
            _controller.Jump();

        _wait = 1f;


     }

}
