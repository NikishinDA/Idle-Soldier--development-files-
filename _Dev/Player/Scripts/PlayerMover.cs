using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private CharacterController _cc;
    private PlayerInputManager _inputManager;
    [SerializeField] private float strafeSpeed;
    [SerializeField] private float movementBorder;
    [SerializeField] private float gravityForce;
    private float _newX;
    private bool _isActive = true;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _inputManager = GetComponent<PlayerInputManager>();
        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
    }
    private void OnGameOver(GameOverEvent obj)
    {
        _isActive = false;
    }

    private void Update()
    {
        if (_isActive)
        {
            _newX = strafeSpeed * _inputManager.GetTouchDelta();
            if (Mathf.Abs(transform.position.x + _newX) > movementBorder)
            {
                _newX = 0;
            }

            _cc.Move(Vector3.right * (_newX) + Vector3.down * (gravityForce * Time.deltaTime));
        }
    }
}
