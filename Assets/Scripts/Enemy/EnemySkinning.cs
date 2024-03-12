using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySkinning : MonoBehaviour
{

    public Enemy enemy;

    [SerializeField] private List<GameObject> manHairs;
    [SerializeField] private List<GameObject> manBackAccessories;
    [SerializeField] private List<GameObject> manCaps;
    [SerializeField] private List<Slap> manSlaps;


    [SerializeField] private List<GameObject> girlHairs;
    [SerializeField] private List<GameObject> girlBackAccessories;
    [SerializeField] private List<GameObject> girlCaps;
    [SerializeField] private List<Slap> girlSlaps;

    [Header("Gender")]
    [SerializeField] private GameObject manObject;
    [SerializeField] private GameObject girlObject;
    [SerializeField] private Avatar girlAvatar;
    [SerializeField] private Avatar manAvatar;
    [SerializeField] private Animator animator;
    private bool isMan;

    [Header("Girl Materials")]
    [SerializeField] private SkinnedMeshRenderer foot;
    [SerializeField] private SkinnedMeshRenderer leftArm;
    [SerializeField] private SkinnedMeshRenderer rightArm;
    [SerializeField] private SkinnedMeshRenderer legs;
    [SerializeField] private SkinnedMeshRenderer torse;
    [SerializeField] private SkinnedMeshRenderer head;

    [Header("Man Materials")]
    [SerializeField] private SkinnedMeshRenderer manMeshRenderer;

    [Header("UI")]
    public bool isNeedName = true;
    [SerializeField] private GameObject uiHandler;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Transform targetToRotate;

    private void Start()
    {
        ChangeGender();
        ChangeClother();
        ChangeMaterial();

        if (isNeedName)
        {
            nameText.text = Helper.GetRandomName();
        }
        else
        {
            nameText.gameObject.SetActive(false);
        }

        targetToRotate = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    private void Update()
    {
        RotateUI();
    }
    private void ChangeClother()
    {
        if (isMan)
        {
            manHairs[Random.Range(0, manHairs.Count - 1)].SetActive(true);
            manBackAccessories[Random.Range(0, manBackAccessories.Count - 1)].SetActive(true);
            manCaps[Random.Range(0, manCaps.Count - 1)].SetActive(true);

            if(Geekplay.Instance.currentMode != Modes.Hub)
            {
                Slap slap = manSlaps[Random.Range(0, manSlaps.Count)];
                slap.gameObject.SetActive(true);
                enemy.slap = slap;
            }
        }
        else
        {
            girlHairs[Random.Range(0, girlHairs.Count - 1)].SetActive(true);
            girlBackAccessories[Random.Range(0, girlBackAccessories.Count - 1)].SetActive(true);
            girlCaps[Random.Range(0, girlCaps.Count - 1)].SetActive(true);

            if (Geekplay.Instance.currentMode != Modes.Hub)
            {
                Slap slap = girlSlaps[Random.Range(0, girlSlaps.Count)];
                slap.gameObject.SetActive(true);
                enemy.slap = slap;
            }
        }
    }

    private void ChangeGender()
    {
        if(Random.Range(0,10) > 5f)
        {
            isMan = false;
            animator.avatar = girlAvatar;
            manObject.gameObject.SetActive(false);
            girlObject.gameObject.SetActive(true);
        }
        else
        {
            isMan = true;
            animator.avatar = manAvatar;
            manObject.gameObject.SetActive(true);
            girlObject.gameObject.SetActive(false);
        }
    }

    private void ChangeMaterial()
    {
        if (isMan)
        {
            ChangeManMaterial();
        }
        else
        {
            ChangeGirlMaterial();
        }
    }

    private void ChangeGirlMaterial()
    {
        Material footMaterial = new Material(Shader.Find("Standard"));
        Material armsMaterial = new Material(Shader.Find("Standard"));
        Material legsMaterial = new Material(Shader.Find("Standard"));
        Material torseMaterial = new Material(Shader.Find("Standard"));
        Material headMaterial = new Material(Shader.Find("Standard"));

        headMaterial.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        footMaterial.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        legsMaterial.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        torseMaterial.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        armsMaterial.color = headMaterial.color;


        ChangeMaterail(foot, 0, footMaterial);
        ChangeMaterail(foot, 1, legsMaterial);

        ChangeMaterail(leftArm, 0, torseMaterial);
        ChangeMaterail(leftArm, 1, armsMaterial);

        ChangeMaterail(rightArm, 0, torseMaterial);
        ChangeMaterail(rightArm, 1, armsMaterial);

        ChangeMaterail(legs, 0, footMaterial);
        ChangeMaterail(legs, 1, legsMaterial);

        ChangeMaterail(head, 0, headMaterial);

        ChangeMaterail(torse, 0, torseMaterial);
    }

    private void ChangeManMaterial()
    {
        Material footMaterial = new Material(Shader.Find("Standard"));
        Material armsMaterial = new Material(Shader.Find("Standard"));
        Material legsMaterial = new Material(Shader.Find("Standard"));
        Material torseMaterial = new Material(Shader.Find("Standard"));
        Material headMaterial = new Material(Shader.Find("Standard"));

        headMaterial.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        footMaterial.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        legsMaterial.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        torseMaterial.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        armsMaterial.color = headMaterial.color;

        ChangeMaterail(manMeshRenderer, 0, armsMaterial);
        ChangeMaterail(manMeshRenderer, 1, torseMaterial);
        ChangeMaterail(manMeshRenderer, 2, legsMaterial);
        ChangeMaterail(manMeshRenderer, 3, footMaterial);
        ChangeMaterail(manMeshRenderer, 4, headMaterial);
    }

    private void ChangeMaterail(SkinnedMeshRenderer renderer, int matIndex, Material mat)
    {
        Material[] materials = renderer.materials;

        materials[matIndex] = mat;
        
        renderer.materials = materials;
    }

    private void RotateUI()
    {
        Vector3 forward = (targetToRotate.position - uiHandler.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);
        Vector3 euler = rotation.eulerAngles;
        uiHandler.transform.rotation = Quaternion.Euler(0, euler.y, 0);
    }
}
