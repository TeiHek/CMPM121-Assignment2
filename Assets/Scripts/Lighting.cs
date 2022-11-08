using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    public List<Light> lights;
    private List<float> baseIntensity = new List<float>();
    public float LightSlider;

    // A lot of the code is from the DayNightScript provided in Canvas. I modified it for the extra credit
    //based on https://youtube.com/watch?v=33RL196x4LI
    [Range(0.0f, 1.0f)]
    public float time;
    public float fullDayLength;
    public float startTime = 0.4f;
    private float timeRate;
    public Vector3 noon;

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    // Start is called before the first frame update
    void Start()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;
        GetBaseIntensity();
    }

    // Update is called once per frame
    void Update()
    {
        float intensityScale = UIElements.Instance.GetSliderValue();
        time += timeRate * Time.deltaTime;
        time %= 1.0f;

        // rotating the planets over time
        // sun is opposite to moon (difference of .5)
        sun.transform.eulerAngles = (time - 0.25f) * noon * 4.0f;
        moon.transform.eulerAngles = (time - 0.75f) * noon * 4.0f;

        // light intensity by evaluating animation curve
        sun.intensity = intensityScale * sunIntensity.Evaluate(time);
        moon.intensity = intensityScale * moonIntensity.Evaluate(time);

        for(int i = 0; i < lights.Count; i++)
        {
            lights[i].intensity = intensityScale * baseIntensity[i];
        }
    }

    private void GetBaseIntensity()
    {
        foreach(Light light in lights)
        {
            baseIntensity.Add(light.intensity);
        }
    }
}
