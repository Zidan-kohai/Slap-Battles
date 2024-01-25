using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] public int sceneIndex;

    private void OnTriggerEnter(Collider other)
    {
        SceneLoader sceneLoader = new SceneLoader();

        sceneLoader.LoadSscene(sceneIndex, null);
    }
}
