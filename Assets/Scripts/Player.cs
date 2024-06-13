using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private Rigidbody2D _rb;
    
    private bool _isDead = false;
    private bool _isAimDownSights = false;
    //--------------------------------------------------------------------//
    [SerializeField] FieldOfView _fieldOfView;
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
            
            SetFieldOfView();
            
        }
    }
    void GetInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetMouseButtonDown(1))
        {
            SwapAimSights();
        }
    }

    void SwapAimSights()
    {
        _isAimDownSights = !_isAimDownSights;
        if (_isAimDownSights)
        {
           _fieldOfView.SetFoV(20);
           _fieldOfView.SetViewDistance(5);
        }
        else
        {
            _fieldOfView.SetFoV(90);
            _fieldOfView.SetViewDistance(3);
        }
    }
    void SetFieldOfView()
    {
        Vector3 targetPosition = GetMouseWorldPosition();
        Vector3 aimDirection = (targetPosition - transform.position).normalized;
        _fieldOfView.SetOrigin(transform.position);
        _fieldOfView.SetAimDirection(aimDirection);
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
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        return position;
    }


}
