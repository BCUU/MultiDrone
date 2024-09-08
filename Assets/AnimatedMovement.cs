using UnityEngine;
using DG.Tweening;

public class AnimatedMovement : MonoBehaviour
{
    public enum MovementType
    {
        SineWave,
        RotateX
    }

    public MovementType movementType;
    public float movementRange = 5f;
    public float movementSpeed = 2f;
    public float rotationAngle = 45f;
    public float rotationSpeed = 2f;

    private void OnEnable()
    {
        switch (movementType)
        {
            case MovementType.SineWave:
                StartSineWaveMovement();
                break;
            case MovementType.RotateX:
                StartRotationX();
                break;
        }
    }

    private void StartSineWaveMovement()
    {
        float halfRange = movementRange / 2f;

        transform.DOMoveX(halfRange, movementSpeed)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private void StartRotationX()
    {
        transform.DORotate(new Vector3(rotationAngle, 0, 0), rotationSpeed, RotateMode.LocalAxisAdd)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.InOutSine);
    }
}