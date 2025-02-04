using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    public void SubmitScore(int score)
    {
        var request = new UpdatePlayerStatisticsRequest()
        {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "GlobalLeaderboard",
                    Value = score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSubmitSuccess, OnError);
    }

    private void OnSubmitSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Score submitted successfully!");
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError("Error submitting score: " + error.GenerateErrorReport());
    }
}