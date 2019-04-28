using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class hostGame : MonoBehaviour
{
    private uint playersCount = 2;
    private string roomName;
    private CustomNM networkManager;

    [SerializeField]
    private InputField inputRoomTest;

    void Start()
    {
        networkManager = CustomNM.Instance;
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
    }

    public void SetRoomName()
    {
        roomName = inputRoomTest.text;
    }

    public void CreateRoom()
    {
        if (roomName != "" && roomName != null)
        {
            networkManager.matchMaker.CreateMatch(roomName, playersCount, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
        }
    }
}
