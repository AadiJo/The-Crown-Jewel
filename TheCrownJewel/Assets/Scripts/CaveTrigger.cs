using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CaveTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Light2D globalLight;
    private float intensity;
    private float defaultIntensity;
    [SerializeField] private float targetIntensity = 0.03f;
    private PlayerMovement playerMovement;
    public GameObject background;

    [Range(0f, 100f)]
    public float m_Lightstep = 75;
    private float lightStep = 0.00075f;
    void Start()
    {

        intensity = globalLight.intensity;
        lightStep = m_Lightstep * 0.00001f;
        defaultIntensity = globalLight.intensity;
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        globalLight.intensity = intensity;
        DimLights();
        if (intensity < 0.1)
        {

            background.SetActive(false);

        }

        lightStep = m_Lightstep * 0.00001f;

        if (intensity < targetIntensity)
        {

            intensity = targetIntensity;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        playerMovement.inCave = true;

    }

    private void DimLights()
    {

        if (playerMovement.inCave && intensity > targetIntensity)
        {

            intensity -= lightStep;

        }

    }
}
