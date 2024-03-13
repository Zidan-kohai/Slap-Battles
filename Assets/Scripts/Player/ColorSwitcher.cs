using System.Collections.Generic;
using UnityEngine;

public class ColorSwitcher : MonoBehaviour
{
    [SerializeField] private Material bodyMaterial;
    [SerializeField] private List<Material> LeatherMaterial;
    [SerializeField] private Material hairMaterial;
    [SerializeField] private Material legMaterial;
    [SerializeField] private Material footMaterial;

    [SerializeField] private Color startBodyColor;
    [SerializeField] private Color startLeatherColor;
    [SerializeField] private Color startHairColor;
    [SerializeField] private Color startLegColor;
    [SerializeField] private Color startFootColor;

    [SerializeField] private Color GoldColor;
    [SerializeField] private Slap slap;

    private void Start()
    {
        startBodyColor = bodyMaterial.color;
        startLeatherColor = LeatherMaterial[0].color;
        startHairColor = hairMaterial.color;
        startLegColor = legMaterial.color;
        startFootColor = footMaterial.color;
    }

    public void GoldPowerActivated()
    {
        startBodyColor = bodyMaterial.color;
        startLeatherColor = LeatherMaterial[0].color;
        startHairColor = hairMaterial.color;
        startLegColor = legMaterial.color;
        startFootColor = footMaterial.color;

        bodyMaterial.color = GoldColor;
        hairMaterial.color = GoldColor;
        legMaterial.color = GoldColor;
        footMaterial.color = GoldColor;

        foreach (Material mat in LeatherMaterial)
        {
            mat.color = GoldColor;
        }
    }

    public void GoldPowerDiactivated()
    {
        bodyMaterial.color = startBodyColor;
        hairMaterial.color = startHairColor;
        legMaterial.color = startLegColor;
        footMaterial.color = startFootColor;

        foreach (Material mat in LeatherMaterial)
        {
            mat.color = startLeatherColor;
        }
    }
}
