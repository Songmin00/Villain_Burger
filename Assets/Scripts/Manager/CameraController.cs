using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed;
    [SerializeField] private Vector3[] _targetPos;
    public int CurrentFocus {  get; private set; }
    public int IndexLength { get; private set; }
    
    private Camera _camera;
    private Vector3 _currentTargetPos;

    private void Awake()
    {
        CurrentFocus = 0;
        IndexLength = _targetPos.Length;
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 currentPos = _camera.transform.position;
        
        if (Vector3.Distance(currentPos, _currentTargetPos) < 0.01f)
        {            
            _camera.transform.position = _currentTargetPos;
            
            this.enabled = false;
            return;
        }        
        _camera.transform.position = Vector3.Lerp(currentPos, _currentTargetPos, _cameraSpeed * Time.deltaTime);
    }    

    public void SetTargetPos(int index)
    {
        _currentTargetPos = _targetPos[index];
        CurrentFocus = index;
    }
}
