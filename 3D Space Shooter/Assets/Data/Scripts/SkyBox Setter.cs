using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class SkyBoxSetter : MonoBehaviour
{
    [SerializeField] List<Material> _skyboxMaterials;

    private void OnEnable()
    {
        ChangeSkybox(0);
    }

    void ChangeSkybox(int skyBox)
    {
        if(skyBox>=0 && skyBox <= _skyboxMaterials.Count)
        {
            RenderSettings.skybox = _skyboxMaterials[skyBox];
        }
    }
}
