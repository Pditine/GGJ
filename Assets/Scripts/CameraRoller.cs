using System.Collections;
using System.Collections.Generic;
using tools;
using UnityEngine;

public class CameraRoller : MonoSingleton<CameraRoller>
{
    public float speed = 1.6f;
    public Transform group;
    public Vector3 lastPos;
    public Vector3 mouseDelta;
    private Transform _camera;
    public float a;

    void Start()
    {
        if (Camera.main != null) _camera = Camera.main.transform;
        //_camera.position= _camera.position - _camera.position.y * Vector3.up + (-335 - GetHeight()) * Vector3.up;
        _camera.position = new Vector3(960, 540, -10);
    }
    void Update()
    {
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        mouseDelta = Input.mousePosition - lastPos;

        lastPos = Input.mousePosition;
        
        if (!Input.GetMouseButton(0))
        {
            if (_camera.position.y>540)
            {
                _camera.position = new Vector3(960, 540, -10);
            }
            return;
        }
        
        if (mouseDelta.magnitude > 0&&_camera.position.y<=540)
        {
            _camera.position += Vector3.down * (mouseDelta.y * speed);
            if (_camera.position.y> group.GetChild(0).transform.position.y)
            {
                _camera.position = _camera.position - _camera.position.y * Vector3.up + group.GetChild(0).transform.position.y * Vector3.up;
            }
            if (_camera.position.y < (group.GetChild(0).transform.position.y-GetHeight())+a)
            {
                _camera.position= _camera.position - _camera.position.y * Vector3.up + (group.GetChild(0).transform.position.y - GetHeight()+a) * Vector3.up;
            }
        }
    }

    private float GetHeight()
    {
        float height = Mathf.Clamp((group.childCount-1),0,group.childCount-2) * group.GetChild(0).GetComponent<RectTransform>().rect.height;
        return height;
    }

    public void ToBottom()
    {
        _camera.position= _camera.position - _camera.position.y * Vector3.up + (group.GetChild(0).transform.position.y - GetHeight()) * Vector3.up;
    }
}
