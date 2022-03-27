using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum State
    {
        Walk, Win, Lose
    }

    [SerializeField]
    private float _changeYSpeed = 10;
    [Space(5)]
    [SerializeField] private GameObject _playerVirtualCamera;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private Ragdoll _ragdoll;
    [SerializeField] private GameObject _lefToe;
    [SerializeField] private GameObject _rightToe;

    private readonly Stack<GameObject> _leftPlatforms = new Stack<GameObject>();
    private readonly Stack<GameObject> _rightPlatforms = new Stack<GameObject>();
    private readonly List<GameObject> _removedPlatforms = new List<GameObject>();

    private int _platformCount;

    public State CurrentState { get; private set; }
    public int Height { get; set; }

    public void Win()
    {
        CurrentState = State.Win;
    }

    public void ChangePlatformCount(int count)
    {
       // _playerVirtualCamera.transform.Translate(count*Vector3.down*.5f);
        if (count > 0)
        {
            AddPlatforms(count);
        }
        else
        {
            count = Mathf.Abs(count);
            RemovePlatforms(count);
        }
    }

    public IEnumerator Play()
    {
        _animator.SetTrigger("Walk");
        _playerMover.enabled = true;
        _playerVirtualCamera.gameObject.SetActive(true);
        while (CurrentState == State.Walk)
        {
            yield return null;
        }

        _playerMover.enabled = false;
        if (CurrentState == State.Lose)
        {
            _ragdoll.ActivateRagdoll();
        }
        else 
        {
            _playerVirtualCamera.gameObject.SetActive(false);
            _animator.SetTrigger("Win");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangePlatformCount(1);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            ChangePlatformCount(-1);
        }


        var pos = transform.position;
        pos.y = Height + Mathf.Clamp(_platformCount, 0, int.MaxValue);
        transform.position = Vector3.MoveTowards(transform.position, pos, _changeYSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        _lefToe.transform.rotation = Quaternion.identity;
        _rightToe.transform.rotation = Quaternion.identity;
    }

    private void RemovePlatforms(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (_platformCount > 0)
            {
                void RemovePlatform(Stack<GameObject> stack)
                {
                    var item2Remove = stack.Pop();
                    item2Remove.transform.parent = null;
                    _removedPlatforms.Add(item2Remove);
                }

                RemovePlatform(_leftPlatforms);
                RemovePlatform(_rightPlatforms);
            }
            else
            {
                CurrentState = State.Lose;
            }

            _platformCount--;
        }
    }

    private void AddPlatforms(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _platformCount++;

            void AddPlatform(Transform parent, Stack<GameObject> stack)
            {
                var platform = Instantiate(_platformPrefab, parent);
                var pos = platform.transform.localPosition;
                pos.y = -_platformCount + 1;
                platform.transform.localPosition = pos;
                stack.Push(platform);
            }

            AddPlatform(_lefToe.transform, _leftPlatforms);
            AddPlatform(_rightToe.transform, _rightPlatforms);
        }
    }

    private void OnDestroy()
    {
        foreach (var item in _removedPlatforms)
        {
            if (item)
                Destroy(item.gameObject);
        }
    }
}