using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private Rigidbody2D _rb;
    
    private bool _isDead = false;
    //--------------------------------------------------------------------//
    
    [SerializeField] Animator _animator;
    [SerializeField] float _moveSpeed = 5f;
    
    private void Awake()
    {
        StartCoroutine(SelfUpdateCoroutine());
        _rb = GetComponent<Rigidbody2D>();
            
    }

    private IEnumerator SelfUpdateCoroutine()
    {
        while (!_isDead)
        {
            yield return null;
            GetInput();
            TryMove();
        }
    }
    void GetInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }
    private void OnDeath()
    {
        _isDead = true;
    }
    void TryMove()
    {
        Vector2 movement = new Vector2(horizontal, vertical);
        _rb.velocity = movement * _moveSpeed;
    }


}
