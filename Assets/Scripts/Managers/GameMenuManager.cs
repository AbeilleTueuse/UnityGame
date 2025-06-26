using FishNet;
using FishNet.Managing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameMenuManager : MonoBehaviour
{
    private NetworkManager _networkManager;
    public UIDocument GameMenuDocument;

    private void Awake()
    {
        _networkManager = InstanceFinder.NetworkManager;
        Button BackToMenu = GameMenuDocument.rootVisualElement.Q<Button>(UIElementNames.BackToMenu);

        if (BackToMenu != null)
        {
            BackToMenu.clicked += OnClick_ReturnToMenu;
        }
    }

    public void OnClick_ReturnToMenu()
    {
        LobbyManager.LeaveLobbyFromGame();
        SceneManager.LoadScene(SceneNames.MainMenu);
    }
}
