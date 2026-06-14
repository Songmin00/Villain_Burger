using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 실제 버튼과 InputHandler를 연결하는 커넥터.
/// 이벤트 연결 및 버튼 활성/비활성화 담당
/// </summary>

public class UpDownButtonConnecter : MonoBehaviour
{
    [SerializeField] InputHandler _handler;

    [SerializeField] Button _upButton;
    [SerializeField] Button _downButton;    

    private void Start()
    {
        _upButton.onClick.RemoveAllListeners();
        _downButton.onClick.RemoveAllListeners();

        _upButton.onClick.AddListener(OnUpButtonClick);
        _downButton.onClick.AddListener(OnDownButtonClick);

        OnUpButtonClick();
    }

    private void OnDestroy()
    {
        _upButton.onClick.RemoveAllListeners();
        _downButton.onClick.RemoveAllListeners();
    }

    private void OnUpButtonClick()
    {
        _handler.OnUpButtonClick();
        _upButton.interactable = false;
        _downButton.interactable = true;
    }

    private void OnDownButtonClick()
    {
        _handler.OnDownButtonClick();
        _downButton.interactable = false;
        _upButton.interactable = true;
    }
}
