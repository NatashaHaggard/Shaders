using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialBlockTest : MonoBehaviour
{
    private MaterialPropertyBlock materialBlock;
    private MeshRenderer meshRenderer;

    void Start()
    {
        materialBlock = new MaterialPropertyBlock();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void ChangePropertyBlock(float amount)
    {
        //create propertyblock only if none exists
        if (materialBlock == null)
            materialBlock = new MaterialPropertyBlock();

        //before setting property blocks, you should GetPropertyBlock first or
        //if you have multiple things modifying those values they will lose properties
        meshRenderer.GetPropertyBlock(materialBlock);

        materialBlock.SetFloat("_PupilRadius", amount);
        meshRenderer.SetPropertyBlock(materialBlock);
    }
}
