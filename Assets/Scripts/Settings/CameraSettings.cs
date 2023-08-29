using UnityEngine;

[CreateAssetMenu(fileName = "CameraSettings", menuName = "Settings/Camera")]
public class CameraSettings : ScriptableObject
{
    [SerializeField, Range(0, 90)] private float _xMaxRotation;
    [SerializeField, Range(0, 90)] private float _yMaxRotation;

    [SerializeField] private float _fieldOfViewEpsilon = 0.5f;

    public float XMaxRotation => _xMaxRotation;
    public float YMaxRotation => _yMaxRotation;
    public float FieldOfViewEpsilon => _fieldOfViewEpsilon;
}
