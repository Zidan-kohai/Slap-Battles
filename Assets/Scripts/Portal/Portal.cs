using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneLoader sceneLoader = new SceneLoader();

        sceneLoader.LoadSscene(1, null);
    }
}
