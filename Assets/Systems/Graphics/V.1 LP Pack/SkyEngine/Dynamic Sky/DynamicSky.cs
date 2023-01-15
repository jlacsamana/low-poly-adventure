using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DynamicSky : MonoBehaviour {
    //this handles the sky

    //reference to global time
    public TimeAndDate globalTD;

    //the sky's material
    public Material skyboxMat;

    //the color gradient for the sky
    public Gradient skyColor;

    //sky objects
    public GameObject skyGroup;
    public GameObject sunObj;
    public GameObject sunActual;
    public GameObject moonActual;
    public GameObject starSystem;

    //light intensity ceiling
    public float sunMaxIntensity = 1.0f;
    public float moonMaxIntensity = 0.25f;

    //day/night cycle boundary
    public float dayNightCycleBoundaryAngle = 90f;
    public float dayNightCycleBoundaryBufferAngle = 30f;
    public float dayCycleTransitionStretchFactor = 1.5f;

    //minimun and maximum atmosphere thickness
    public float maxAtmosphereThiccness = 2.25f;
    public float minAtmosphereThiccness = 1f;

    //minimum and maximum skybox exposure
    public float maxAtmosphereExposure = 4;
    public float minAtmosphereExposure = 1;

    //current cycle part
    public enum DayCycle { Day, Night };
    public DayCycle currentCycleTime = DayCycle.Day;

    public enum DayCycleTransition {Dawn, Dusk };
    public DayCycleTransition currentCycleTransition = DayCycleTransition.Dawn;

    // Use this for initialization
    void Start () {
        skyboxMat = RenderSettings.skybox;

    }

    
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 1)
        {
            UpdateSun();

        }
	}

    //update sun
    public void UpdateSun()
    {
        if (sunObj != null)
        {
            sunObj.transform.Rotate(0,0,- (360/(globalTD.dayLength * globalTD.dayLengthScale )) * Time.deltaTime);
            sunActual.transform.LookAt(new Vector3(850,0,850));

            float cycleIndicator = Mathf.Abs(180 - sunObj.transform.localEulerAngles.z);
            //controls moonlight and sunlight intensity
            //if night time
            if (cycleIndicator <= (dayNightCycleBoundaryAngle + dayNightCycleBoundaryBufferAngle))
            {
                //if sun is setting
                if (currentCycleTime == DayCycle.Day)
                {
                    if (cycleIndicator >= dayNightCycleBoundaryAngle)
                    {
                        sunActual.GetComponent<Light>().intensity = sunMaxIntensity - 
                        (sunMaxIntensity * (((dayNightCycleBoundaryAngle + dayNightCycleBoundaryBufferAngle) - cycleIndicator)/ dayNightCycleBoundaryBufferAngle));
                        moonActual.GetComponent<Light>().intensity = moonMaxIntensity * (((dayNightCycleBoundaryAngle + dayNightCycleBoundaryBufferAngle) - cycleIndicator) / dayNightCycleBoundaryBufferAngle);
                        //toggles star system visibility
                        float starEmission = (((dayNightCycleBoundaryAngle + dayNightCycleBoundaryBufferAngle) - cycleIndicator) / dayNightCycleBoundaryBufferAngle);
                        starSystem.GetComponent<ParticleSystemRenderer>().material.SetColor("_EmissionColor", new Color(starEmission, starEmission, starEmission, 0));
                    }
                    else if (cycleIndicator < dayNightCycleBoundaryAngle)
                    {
                        sunActual.GetComponent<Light>().intensity = 0;
                        moonActual.GetComponent<Light>().intensity = moonMaxIntensity;
                        currentCycleTime = DayCycle.Night;
                        //toggles star system visibility
                        starSystem.GetComponent<ParticleSystemRenderer>().material.SetColor("_EmissionColor", new Color(1, 1, 1, 0));
                       
                    }
                }
                //if sun is rising
                if (currentCycleTime == DayCycle.Night)
                {
                    if (cycleIndicator >= dayNightCycleBoundaryAngle)
                    {
                        sunActual.GetComponent<Light>().intensity = sunMaxIntensity - (sunMaxIntensity * (((dayNightCycleBoundaryAngle + dayNightCycleBoundaryBufferAngle) - cycleIndicator) / dayNightCycleBoundaryBufferAngle));
                        moonActual.GetComponent<Light>().intensity = moonMaxIntensity * (((dayNightCycleBoundaryAngle + dayNightCycleBoundaryBufferAngle) - cycleIndicator) / dayNightCycleBoundaryBufferAngle);
                        //toggles star system visibility
                        float starEmission = (((dayNightCycleBoundaryAngle + dayNightCycleBoundaryBufferAngle) - cycleIndicator) / dayNightCycleBoundaryBufferAngle);
                        starSystem.GetComponent<ParticleSystemRenderer>().material.SetColor("_EmissionColor", new Color(starEmission, starEmission, starEmission, 0));
                    }

                }  
            }
            //if day time
            else if (cycleIndicator > (dayNightCycleBoundaryAngle + dayNightCycleBoundaryBufferAngle))
            {
                
                sunActual.GetComponent<Light>().intensity = sunMaxIntensity;
                moonActual.GetComponent<Light>().intensity = 0;
                currentCycleTime = DayCycle.Day;
                //toggles star system visibility
                starSystem.GetComponent<ParticleSystemRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0, 0));
            }
            //controls atmosphere thiccness
            //if night
            if (cycleIndicator <= (dayNightCycleBoundaryAngle + (dayNightCycleBoundaryBufferAngle * dayCycleTransitionStretchFactor)))
            {
                if (cycleIndicator >= dayNightCycleBoundaryAngle)
                {
                    //toggles atmospheric thiccness
                    skyboxMat.SetFloat("_AtmosphereThickness", minAtmosphereThiccness + ((maxAtmosphereThiccness - minAtmosphereThiccness) *
                    (((dayNightCycleBoundaryAngle + (dayNightCycleBoundaryBufferAngle * dayCycleTransitionStretchFactor)) - cycleIndicator) / (dayNightCycleBoundaryBufferAngle * dayCycleTransitionStretchFactor))
                    ));
                    //toggles skybox material exposure
                    //skyboxMat.SetFloat("_Exposure", 1);

                }
                else if (cycleIndicator < dayNightCycleBoundaryAngle)
                {
                    skyboxMat.SetFloat("_AtmosphereThickness", maxAtmosphereThiccness);
                }
            }
            //if day
            else if (cycleIndicator > (dayNightCycleBoundaryAngle + (dayNightCycleBoundaryBufferAngle * dayCycleTransitionStretchFactor)))
            {
                skyboxMat.SetFloat("_AtmosphereThickness", minAtmosphereThiccness);
                currentCycleTransition = DayCycleTransition.Dawn;
            }

            //handles the star system
            //refreshes stellar map every noon; so new map can be seen at night
            if (System.Math.Round(cycleIndicator, 1) == 180d)
            {
                starSystem.GetComponent<ParticleSystem>().Clear();
                starSystem.GetComponent<ParticleSystem>().Play();
            }
            //Debug.Log(System.Math.Round(cycleIndicator,1));


        }
    }

    //scene chhange listener
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //finds sun in scene
        skyGroup = GameObject.Find("Environment/Sky");
        sunObj = skyGroup.transform.Find("SunObj").gameObject;
        sunActual = sunObj.transform.Find("Sun").gameObject;
        moonActual = sunObj.transform.Find("Moon").gameObject;
        starSystem = skyGroup.transform.Find("Star System").gameObject;
        
    }


}
