using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(SpringJoint), typeof(Rigidbody))]
public class CatapultHandler : MonoBehaviour
{
    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _reloadButton;
    [SerializeField] private ProjectileSpawner _spawner;
    [SerializeField] private float _verticalAmplitude;
    [SerializeField] private float _weakingDeley = 0.5f;

    private SpringJoint _spring;
    private Rigidbody _rigidbody;
    private float _springDefaultValue;

    private bool isAttackPressed;
    private bool isReloadPressed;

    private void Awake()
    {
        _spring = GetComponent<SpringJoint>();
        _rigidbody = GetComponent<Rigidbody>();

        _springDefaultValue = _spring.spring;

        isAttackPressed = true;
        isReloadPressed = false;
    }

    private void OnEnable()
    {
        _attackButton.onClick.AddListener(Attack);
        _reloadButton.onClick.AddListener(Reload);
    }

    private void OnDisable()
    {
        _attackButton.onClick.RemoveListener(Attack);
        _reloadButton.onClick.RemoveListener(Reload);
    }

    private void Attack()
    {
        if (_verticalAmplitude < 0)
            throw new ArgumentOutOfRangeException("Vertical amplitude is negative");

        if (isReloadPressed == false) 
            return;

        isAttackPressed = true;
        isReloadPressed = false;

        _spring.spring = _springDefaultValue;

        Vector3 anchorPosition = _spring.connectedAnchor;
        anchorPosition.y += _verticalAmplitude;

        if (_rigidbody != null)
            _rigidbody.WakeUp();

        _spring.connectedAnchor = anchorPosition;

        StartCoroutine(SpringWeaking());
    }

    private void Reload()
    {
        if (isAttackPressed == false)
            return;

        isReloadPressed = true;
        isAttackPressed = false;

        if (_rigidbody != null)
            _rigidbody.WakeUp();
                
        _spawner.SpawnProjectile();
    }

    private IEnumerator SpringWeaking()
    {
        if (_weakingDeley < 0)
            throw new ArgumentOutOfRangeException("Weaking deley is negative");

        WaitForSeconds waitForSeconds = new(_weakingDeley);

        yield return waitForSeconds;

        _spring.spring = 0;

        Vector3 anchorPosition = _spring.connectedAnchor;
        anchorPosition.y -= _verticalAmplitude;
        _spring.connectedAnchor = anchorPosition;
    }
}