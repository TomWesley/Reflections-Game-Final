using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabCentralLogin : MonoBehaviour
{
    public static bool IsLoggedIn { get; private set; }

    void Start()
    {
        LoginToPlayFab();
    }

    private void LoginToPlayFab()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = "tomjwesley@gmail.com", // Replace with your shared account email
            Password = "Mydogbarkssome^9"       // Replace with your shared account password
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Successfully logged in to PlayFab!");
        IsLoggedIn = true;
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError($"Failed to log in to PlayFab: {error.GenerateErrorReport()}");
        IsLoggedIn = false;
    }
}