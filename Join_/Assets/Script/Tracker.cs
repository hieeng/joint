using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    Coroutine _coroutineCamera = null;

    private void Start() 
    {
    }

    private void LateUpdate() 
    {
        CameraUp();
        Follow();
    }

    private void CameraUp()
    {
        if (!GameManager.instance.player.isStart)
            return;
        if (_coroutineCamera != null)
            StopCoroutine(_coroutineCamera);
        _coroutineCamera = StartCoroutine(CoroutineCameraUp());
    }

    IEnumerator CoroutineCameraUp()
    {
        float time = 0;

        while (offset.y <= 6.5f)
        {
            time += Time.deltaTime;
            offset.y += time * 1.5f;

            yield return null;
        }
    }
    
    private void Follow()
    {
        transform.position = target.position + offset;
    }
}
