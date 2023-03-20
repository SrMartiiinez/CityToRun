
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelector : MonoBehaviour
{
    [SerializeField] private Button previo;
    [SerializeField] private Button next;
    private int playerSelector;

    private void Awake()
    {
        SelectPlayer(0);
    }

    private void SelectPlayer(int _index)
    {
        previo.interactable = (_index != 0);
        next.interactable = (_index != transform.childCount-1);
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }
    }

    public void ChangeCharacter(int _change)
    {
        playerSelector += _change;
        SelectPlayer(playerSelector);
    }
}
