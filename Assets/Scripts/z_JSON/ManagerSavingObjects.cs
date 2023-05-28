using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSavingObjects : MonoBehaviour {

    [SerializeField] private Character curPlayer;

    /*IEnumerator Start() {
        yield return new WaitForSeconds(0.01f);
        Load();
        Debug.Log($"Loaded position of {curPlayer}");
    }*/

    private void Start() {
        Load();
        Debug.Log($"Loaded position of {curPlayer}");
    }

    public void Save() {
        JsonManager.SaveGame(curPlayer);
    }

    public void Load() {
        JsonManager.LoadGame(curPlayer);
    }

}