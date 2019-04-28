using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class EditRoomList : MonoBehaviour
{
    public delegate void JoinRoomsDeligate(MatchInfoSnapshot _match);
    private JoinRoomsDeligate joinCallBack;

    [SerializeField]
    private Text matchName;
    
    private MatchInfoSnapshot matchInfo;

    public void Setup (MatchInfoSnapshot _match, JoinRoomsDeligate _joinCallBack)
    {
        matchInfo = _match;
        joinCallBack = _joinCallBack;
        matchName.text = matchInfo.name + " (" + matchInfo.currentSize + " / " + matchInfo.maxSize + ")";
    }

    public void JoinRoom()
    {
        joinCallBack.Invoke(matchInfo);
    }
}
