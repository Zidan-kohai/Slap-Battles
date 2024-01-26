using UnityEngine;

public partial class Portal : MonoBehaviour
{
    public int SceneIndex;
    public Modes Mode;
    private void OnTriggerEnter(Collider other)
    {
        SceneLoader sceneLoader = new SceneLoader();

        sceneLoader.LoadSscene(SceneIndex, null);
    }


}