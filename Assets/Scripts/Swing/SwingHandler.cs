using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Rigidbody))]
public class SwingHandler : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private int _force;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(PushSwing);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(PushSwing);
    }

    private void PushSwing()
    {
        if (_force < 0)
            throw new ArgumentOutOfRangeException("Force is negative");

        _rigidbody.AddForce(Vector3.forward * _force);
    }
}