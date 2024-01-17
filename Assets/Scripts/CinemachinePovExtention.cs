using UnityEngine;
using Cinemachine;
public class CinemachinePovExtention : CinemachineExtension
{

    [SerializeField] private PlayerController controller;
    [SerializeField] private float horizontalCameraSpeed = 10;
    private Vector3 initialRotation;

    protected override void Awake()
    {
        initialRotation = transform.localRotation.eulerAngles;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                Vector2 lateralInputs = controller.rotation;
                initialRotation.x += lateralInputs.x * horizontalCameraSpeed * Time.deltaTime;
                state.RawOrientation = Quaternion.Euler(0,initialRotation.x,0);
            }
        }
    }
}
