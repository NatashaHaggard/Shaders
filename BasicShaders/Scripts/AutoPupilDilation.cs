using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AutoPupilDilation : MonoBehaviour
{
    private Material m_Material; // create a private instance variable to hold the Material
    private Light l_Light; // create a private instance variable to hold the Light
    

    private void Start()
    {
        //Fetch the Material from the Renderer of the GameObject
        m_Material = GetComponent<MeshRenderer>().sharedMaterial;

        //Fetch the Light
        l_Light = FindObjectOfType<Light>();

        // Debug code to check if it's looking at the right material
        string materialName = m_Material.name;
        print("Material name is " + name);

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
        float lightIntensity = l_Light.intensity;
        print("Current light intensity is " + lightIntensity);

        // Open ShaderGraph in Unity, see that PupilRadius is a float
        // Get reference for PupilRadius from ShaderGraph: Vector1_DFF948F3. Change it to _PupilRadius

        float pupilSize = m_Material.GetFloat("_PupilRadius"); // get current pupil radius
        print("Current pupil radius is " + pupilSize); // print current pupil radius
     
        float lightIntensityStart = 3000.0f;
        float lightIntensityEnd = 10000.0f;
        float lightCurrentIntensity = lightIntensity;

        // the progress between start and end is stored as a 0-1 value, in 'i'
        float iLight = Mathf.InverseLerp(lightIntensityStart, lightIntensityEnd, lightCurrentIntensity);

        float pupilSizeStart = 0.2f;
        float pupilSizeEnd = 0.6f;
        float pupilCurrentSize = 0.3f;

        // inverse variation calculation
        float pupil = (pupilSizeEnd - pupilSizeStart) * iLight;


        // the progress between start and end is stored as a 0-1 value, in 'i'
        float iPupil = Mathf.InverseLerp(pupilSizeStart, pupilSizeEnd, pupilCurrentSize);

        // this will display "Current progress: 0.1 or 10%" in Console window
        Debug.Log("Current pupil size: " + iPupil);
        Debug.Log("Current light intensity: " + iLight);

        // update pupil size
        m_Material.SetFloat("_PupilRadius", pupil); 
    }
}
