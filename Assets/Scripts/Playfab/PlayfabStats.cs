﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class PlayfabStats : MonoBehaviour
{

    private int totalscore;

    private void Start()
    {
        if (PlayerPrefs.HasKey("TOTALSCORE"))
        {
            totalscore = PlayerPrefs.GetInt("TOTALSCORE");
        }
        else
        {
            PlayerPrefs.SetInt("TOTALSCORE", 0);
        }
    }

    public void Sendscore()
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
               new StatisticUpdate
               {
                    StatisticName="Overall Score",
                    Value = PlayerPrefs.GetInt("TOTALSCORE")
               }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, Onsuccessupdate, Onerror);
    }

    private void Onsuccessupdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("User statistics updated");
    }

    public void Getscore()
    {
        PlayFabClientAPI.GetPlayerStatistics(new GetPlayerStatisticsRequest(), OnGetscore, Onerror);
    }

    private void OnGetscore(GetPlayerStatisticsResult result)
    {
        foreach (var eachStat in result.Statistics)
        {
            switch (eachStat.StatisticName)
            {
                case "Overall Score":
                    totalscore = eachStat.Value;
                    break;
            }
        }

        PlayerPrefs.SetInt("TOTALSCORE", totalscore);
    }

    private void Onerror(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
}

