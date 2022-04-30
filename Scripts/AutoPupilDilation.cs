using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AutoPupilDilation : MonoBehaviour
{
    private Material m_Material; // create a private instance variable to hold the Material
    private Light m_Light; // create a private instance variable to hold the Light

    private const float pupilSizeStart = 0.2f;
    private const float pupilSizeEnd = 0.6f;
    private const float pupilCurrentSize = 0.3f;

    private const float lightIntensityStart = 3000.0f;
    private const float lightIntensityEnd = 10000.0f;

    private void Start()
    {
        //Fetch the Material from the Renderer of the GameObject
        m_Material = GetComponent<MeshRenderer>().sharedMaterial;

        //Fetch the Light
        m_Light = FindObjectOfType<Light>();

        // Debug code to check if it's looking at the right material
        string materialName = m_Material.name;
        Debug.Log("Material name is " + name);

        PupilDilation(); // call the pupil dilation method
    }

    // Update is called once per frame
    void Update()
    {
        PupilDilation(); // call the pupil dilation method
    }

    // create method to dilate pupil size based on light intensity
    public void PupilDilation()
    {
        float lightIntensity = m_Light.intensity;
        print("Current light intensity is " + lightIntensity);

        // Open ShaderGraph in Unity, see that PupilRadius is a float
        // Get reference for PupilRadius from ShaderGraph: Vector1_DFF948F3. Change it to _PupilRadius

        float pupilSize = m_Material.GetFloat("_PupilRadius"); // get current pupil radius
        Debug.Log("Current pupil radius is " + pupilSize); // print current pupil radius  
       
        float lightCurrentIntensity = lightIntensity;

        // the progress between start and end is stored as a 0-1 value, in 'i'
        float iLight = Mathf.InverseLerp(lightIntensityStart, lightIntensityEnd, lightCurrentIntensity);

        // inverse variation calculation
        float pupil = (pupilSizeEnd - pupilSizeStart) * iLight;

        // the progress between start and end is stored as a 0-1 value, in 'i'
        float iPupil = Mathf.InverseLerp(pupilSizeStart, pupilSizeEnd, pupilCurrentSize);

        // this will display the current pupil size and light intensity in Console window
        Debug.Log("Current pupil size: " + iPupil);
        Debug.Log("Current light intensity: " + iLight);

        // update pupil size
        m_Material.SetFloat("_PupilRadius", pupil); 
    }
}
