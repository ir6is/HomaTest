using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody _mainRigidbody;
    [SerializeField] private Collider _mainCollider;
    [SerializeField] private Animator _animator;

    [SerializeField] private List<Rigidbody> _rigidBodies = new List<Rigidbody>();
    [SerializeField] private List<Collider> _colliders = new List<Collider>();

    [ContextMenu("ActivateRagdoll")]
    public void ActivateRagdoll()
    {
        _animator.enabled = false;
        _mainRigidbody.isKinematic = true;
        _mainCollider.enabled = false;
        foreach (var item in _rigidBodies)
        {
            item.isKinematic = false;
        }

        foreach (var item in _colliders)
        {
            item.enabled = true;
        }
    }

    [ContextMenu("Update")]
    private void UpdateData()
    {
        _colliders.Clear();
        foreach (var item in GetComponentsInChildren<Collider>())
        {
            if (item != _mainCollider)
            {
                item.enabled = false;
                _colliders.Add(item);
            }
        }

        _rigidBodies.Clear();
        foreach (var item in GetComponentsInChildren<Rigidbody>())
        {
            if (item != _mainRigidbody)
            {
                item.isKinematic = true;
                _rigidBodies.Add(item);
            }
        }
    }

    [ContextMenu("Clear")]
    private void ClearData()
    {
        foreach (var item in GetComponentsInChildren<Joint>())
        {
                DestroyImmediate(item);
        }

        foreach (var item in GetComponentsInChildren<Collider>())
        {
            if (item != _mainCollider)
            {
                DestroyImmediate(item);
            }
        }

        foreach (var item in GetComponentsInChildren<Rigidbody>())
        {
            if (item != _mainRigidbody)
            {
                DestroyImmediate(item);
            }
        }
    }
}