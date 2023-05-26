using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSavingObjects : MonoBehaviour {

    [SerializeField] private Player curPlayer;
    [SerializeField] private Enemy[] enemies;

    private void Start() {
        Load();
    }

    public void Save() {
        JsonManager.SaveGame(curPlayer, enemies);
    }

    public void Load() {
        JsonManager.LoadGame(curPlayer, enemies);
    }

}
