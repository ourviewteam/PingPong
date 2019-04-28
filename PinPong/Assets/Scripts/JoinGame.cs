using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System;

public class JoinGame : NetworkBehaviour
{
    private CustomNM networkManager;
    List<GameObject> matchList = new List<GameObject>();

    [SerializeField]
    private GameObject listItemGO;
    [SerializeField]
    private Transform scrollView;

    private void Start()
    {
        networkManager = CustomNM.Instance;
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
        RefreshList();
    }

    public void RefreshList()
    {
        networkManager.matchMaker.ListMatches(0, 15, "", true, 0,0, OnMatchList);
    }

    private void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (matches == null)
        {
            return;
        }
        ClearRoomList();
        foreach (MatchInfoSnapshot match in matches)
        {
            GameObject ListItem = Instantiate(listItemGO);
            ListItem.transform.SetParent(scrollView);
            ListItem.transform.localScale = new Vector3(1, 1, 1);

            EditRoomList _item = ListItem.GetComponent<EditRoomList>();
            if (_item != null)
            {
                _item.Setup(match, JoiningRoom);
            }
            matchList.Add(ListItem);

        }
    }

    private void ClearRoomList()
    {
        for (int i = 0; i< matchList.Count;i++)
        {
            Destroy(matchList[i]);
        }
        matchList.Clear();
    }

    public void JoiningRoom(MatchInfoSnapshot match)
    {
        networkManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
        ClearRoomList();
    }
}
