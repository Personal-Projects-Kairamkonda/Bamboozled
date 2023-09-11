using UnityEngine.UI;
using TMPro;

public class Constants
{
    public readonly static string loadingScene="LoadingScene";
    public readonly static string lobbyScene = "LobbyScene";
    public readonly static string gameScene = "Playground";
}

[System.Serializable]
public class SettingsData
{
    public Button button;
    public TMP_InputField inputField;
}
