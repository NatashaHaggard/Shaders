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

        materialBlock.SetFloat("_PupilRadius", amount);
        meshRenderer.SetPropertyBlock(materialBlock);
    }
}
