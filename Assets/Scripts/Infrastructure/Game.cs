using UnityEngine;

public class Game 
{
    private SceneLoader sceneLoader;

    public Game(MonoBehaviour mono)
    {
        sceneLoader  = new SceneLoader(mono);
    }
}
