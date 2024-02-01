using System;
using System.Collections.Generic;
using UnityEngine;

public class ModeSpawner : MonoBehaviour
{
    [SerializeField] private List<GameMode> gameModes = new List<GameMode>();
    void Start()
    {
        for(int i = 0; i < gameModes.Count; i++)
        {
            if (gameModes[i].mode == Geekplay.Instance.currentMode)
            {
                Instantiate(gameModes[i].gameObject);
            }
        }
    }

    [Serializable]
    public class GameMode
    {
        public Modes mode;
        public GameObject gameObject;
    }
}


