using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public partial class Portal : MonoBehaviour
{
    public int SceneIndex;
    public Modes Mode;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7) return;
        SceneLoader sceneLoader = new SceneLoader();

        sceneLoader.LoadSscene(SceneIndex, () =>
        {
            Geekplay.Instance.currentMode = Mode;
        });
    }


}