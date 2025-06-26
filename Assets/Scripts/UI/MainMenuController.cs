using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject LoadGamePanel;
    public GameObject StartGamePanel;
    public GameObject ManageLobbyPanel;
    public GameObject LobbyListPanel;
    public GameObject LoadGameContent;
    public GameObject LobbyListContent;
    public Button LoadGameButtonPrefab;
    public Button ContinueGameButton;
    public Button LoadGameButton;
    public Button NewGameButton;
    public Button StartGameButton;
    public Button InviteFriendsButton;

    public void ShowLoadGamePanel()
    {
        StartPanel.SetActive(false);
        LoadGamePanel.SetActive(true);
    }

    public void HideLoadGamePanel()
    {
        StartPanel.SetActive(true);
        LoadGamePanel.SetActive(false);
    }

    public void ClearLoadGameContent()
    {
        foreach (Transform child in LoadGameContent.transform)
            Destroy(child.gameObject);
    }

    public void ClearLobbyListContent()
    {
        foreach (Transform child in LoadGameContent.transform)
            Destroy(child.gameObject);
    }

    public void CreateLoadGameButton(
        PlayerSaveData playerSaveData,
        System.Action<PlayerSaveData> onClick
    )
    {
        Button button = Instantiate(LoadGameButtonPrefab, LoadGameContent.transform);
        button.GetComponentInChildren<TextMeshProUGUI>().text = playerSaveData.GetDisplayName();
        button.onClick.AddListener(() => onClick.Invoke(playerSaveData));
    }

    public void CreateLobbyButton(LobbyInfo lobby, System.Action<LobbyInfo> onClick)
    {
        Button button = Instantiate(LoadGameButtonPrefab, LobbyListContent.transform);
        button.GetComponentInChildren<TextMeshProUGUI>().text = lobby.Name;
        button.onClick.AddListener(() => onClick.Invoke(lobby));
    }

    public void UpdateButtons(bool playerHasNoSave)
    {
        if (playerHasNoSave)
        {
            ContinueGameButton.gameObject.SetActive(false);
            LoadGameButton.gameObject.SetActive(false);
            ChangeCurrentButton(NewGameButton);
        }
        else
        {
            ContinueGameButton.gameObject.SetActive(true);
            LoadGameButton.gameObject.SetActive(true);
            ChangeCurrentButton(ContinueGameButton);
        }
    }

    public void ShowStartGamePanel()
    {
        StartPanel.SetActive(false);
        StartGamePanel.SetActive(true);
        ManageLobbyPanel.SetActive(true);
        StartGameButton.gameObject.SetActive(false);
        InviteFriendsButton.gameObject.SetActive(false);
    }

    public void HideStartGamePanel()
    {
        StartPanel.SetActive(true);
        StartGamePanel.SetActive(false);
    }

    public void ShowLobbyListPanel()
    {
        LobbyListPanel.SetActive(true);
        StartGamePanel.SetActive(false);
    }

    public void HideLobbyListPanel()
    {
        LobbyListPanel.SetActive(false);
        StartGamePanel.SetActive(true);
    }

    public void StartLobbyCreation()
    {
        UILoadingPanel.Instance.Show();
    }

    public void LobbyIsCreated()
    {
        UILoadingPanel.Instance.Hide();
        ManageLobbyPanel.SetActive(false);
        StartGameButton.gameObject.SetActive(true);
        InviteFriendsButton.gameObject.SetActive(true);
    }

    public void LobbyCreationFailed()
    {
        UILoadingPanel.Instance.Hide();
    }

    public void StartLeavingLobby()
    {
        UILoadingPanel.Instance.Show();
    }

    public void LobbyLeaveCompleted()
    {
        UILoadingPanel.Instance.Hide();
        ManageLobbyPanel.SetActive(true);
        StartGameButton.gameObject.SetActive(false);
        InviteFriendsButton.gameObject.SetActive(false);
    }

    public void StartLobbySearch()
    {
        UILoadingPanel.Instance.Show();
        ShowLobbyListPanel();
    }

    public void LobbyListIsCreated()
    {
        UILoadingPanel.Instance.Hide();
    }

    private static void ChangeCurrentButton(Selectable selectable)
    {
        if (selectable == null)
        {
            Debug.LogWarning("Button is null, cannot change current button.");
            return;
        }

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectable.gameObject);
    }
}
