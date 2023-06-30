using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveManager : MonoBehaviour
{
    [SerializeField] private float _shockWaveTime = 0.75f;

    private Coroutine _shockWaveCoroutine;

    private Material _material;

    private static int _waveDistanceFromCenter = Shader.PropertyToID("_WaveDistanceFromCenter");

    private void Awake()
    {

        _material = GetComponent<SpriteRenderer>().material;

    }

    private void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {

            CallShockWave();

        }

    }

    public void CallShockWave()
    {

        _shockWaveCoroutine = StartCoroutine(ShockWaveAction(-1.0f, 1f));

    }

    private IEnumerator ShockWaveAction(float startPos, float endPos)
    {

        _material.SetFloat(_waveDistanceFromCenter, startPos);

        float lerpedAmount = 0f;

        float elaspedTime = 0f;
        while (elaspedTime < _shockWaveTime)
        {

            elaspedTime += Time.deltaTime;

            lerpedAmount = Mathf.Lerp(startPos, endPos, (elaspedTime / _shockWaveTime));
            _material.SetFloat(_waveDistanceFromCenter, lerpedAmount);

            yield return null;
        }

    }
}
