using UnityEngine;
using Cinemachine;
public class CinemachinePovExtention : CinemachineExtension
{

    [SerializeField] private Player player;
    [SerializeField] private float horizontalCameraSpeed = 10;
    private Vector3 initialRotation;

    protected override void Awake()
    {
        initialRotation = transform.localRotation.eulerAngles;
        base.Awake();
    }


    /**
     * 
     * Override of abstract method PostPipelineStageCallback() from interface CinemachineExtention
     * 
     * **/

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                // Assign player rotation to a vector 2 variable
                Vector2 lateralInputs = player.rotation;

                // Assign x value to the vector3 variable "initialRotation"
                initialRotation.x += lateralInputs.x * horizontalCameraSpeed * Time.deltaTime;

                // Assign new value of RawOrientation to camera to define an horizontal visual movement
                state.RawOrientation = Quaternion.Euler(0,initialRotation.x,0);
            }
        }
    }
}
