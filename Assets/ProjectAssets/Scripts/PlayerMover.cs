using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Transform _projection;
    [SerializeField] private float _rotationSpeed = 10;
    [SerializeField] private float _maxSideShiftForRotation = .5f;
    [SerializeField] private float _maxRotationAngel = 20;
    [SerializeField] private float _speedLimit = .1f;
    [SerializeField] private float _xSpeed = 1;
    [SerializeField] private float _zSpeed = 1;
    [SerializeField] private float _roadSize = 1;
    [SerializeField] private Transform _view2Rotate;

    private Vector2 _currentInput => new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.width);
    private Vector2 _lastInput;

    private void Update()
    {
        transform.Translate(Vector3.forward * _zSpeed * Time.deltaTime);
        _projection.position = transform.position - Vector3.up * transform.position.y;
    }

    private IEnumerator Start()
    {
        while (true)
        {
            while (!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }

            _lastInput = _currentInput;
            while (Input.GetMouseButton(0))
            {
                var delta = _currentInput - _lastInput;
                var pos = transform.position;
                delta.x = Mathf.Clamp(delta.x, -_speedLimit, _speedLimit);
                pos.x += _xSpeed * delta.x * Time.deltaTime;
                pos.x = Mathf.Clamp(pos.x, -_roadSize, _roadSize);
                transform.position = Vector3.Lerp(transform.position, pos, 0.1f);

                var factor = delta.x / _maxSideShiftForRotation;
                factor = Mathf.Clamp(factor, -1, 1);

                var newRotation = Quaternion.Euler(_view2Rotate.eulerAngles.x, _maxRotationAngel * factor, _view2Rotate.eulerAngles.z);
                _view2Rotate.localRotation = Quaternion.RotateTowards(_view2Rotate.localRotation, newRotation, _rotationSpeed * Time.deltaTime);
                // _view2Rotate.localRotation = newRotation;
                _lastInput = _currentInput;
                yield return null;
            }


            yield return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(Vector3.right * _roadSize, .1f);
        Gizmos.DrawSphere(Vector3.left * _roadSize, .1f);
    }
}