using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] CameraController _cameraController;

    public void OnDownButtonClick()
    {
        if (_cameraController == null)
        {
            Debug.LogError("CameraController가 할당되지 않았습니다.");
            return;
        }
        if (_cameraController.CurrentFocus < _cameraController.IndexLength -1)
        {
            SetCameraFocus(_cameraController.CurrentFocus + 1);
        }
        else
        {
            return;
        }        
    }

    public void OnUpButtonClick()
    {
        if (_cameraController == null)
        {
            Debug.LogError("CameraController가 할당되지 않았습니다.");
            return;
        }
        if (_cameraController.CurrentFocus > 0)
        {
            SetCameraFocus(_cameraController.CurrentFocus - 1);
        }
        else
        {
            return;
        }
    }

    private void SetCameraFocus(int index)
    {
        _cameraController.enabled = true;
        _cameraController.SetTargetPos(index);
    }

    
}
