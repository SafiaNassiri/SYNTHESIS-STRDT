using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public GameObject twinXPrefab;
    public GameObject twinYPrefab;
    public GameObject noodlePrefab;

    private GameObject currentPlayer;

    public void SelectCharacter(string characterName)
    {
        Vector3 spawnPos = Vector3.zero;

        switch (characterName)
        {
            case "TwinX":
                currentPlayer = Instantiate(twinXPrefab, spawnPos, Quaternion.identity);
                break;
            case "TwinY":
                currentPlayer = Instantiate(twinYPrefab, spawnPos, Quaternion.identity);
                break;
        }

        GameObject noodle = Instantiate(noodlePrefab, currentPlayer.transform.position + new Vector3(1, 1, 0), Quaternion.identity);
        noodle.GetComponent<NoodleFollow>().player = currentPlayer.transform;

        Camera.main.GetComponent<CameraFollow>().target = currentPlayer.transform;
    }
}
