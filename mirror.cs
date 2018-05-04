using UnityEngine;
using Passer;

public class CopyTargets : MonoBehaviour {

    public HumanoidControl[] avatars;
    private HumanoidControl thisHumanoid;

    public bool mirror;

	public void Start () {
        thisHumanoid = GetComponent<HumanoidControl>();
	}
	
	public void Update () {
		foreach (HumanoidControl humanoid in avatars) {
            CopyTarget(thisHumanoid.headTarget, humanoid.headTarget);
            if (mirror) {
                CopyTarget(thisHumanoid.leftHandTarget, humanoid.rightHandTarget);
                CopyTarget(thisHumanoid.rightHandTarget, humanoid.leftHandTarget);
            } else {
                CopyTarget(thisHumanoid.leftHandTarget, humanoid.leftHandTarget);
                CopyTarget(thisHumanoid.rightHandTarget, humanoid.rightHandTarget);
            }
            CopyTarget(thisHumanoid.hipsTarget, humanoid.hipsTarget);
            if (mirror) {
                CopyTarget(thisHumanoid.leftFootTarget, humanoid.rightFootTarget);
                CopyTarget(thisHumanoid.rightFootTarget, humanoid.leftFootTarget);
            } else {
                CopyTarget(thisHumanoid.leftFootTarget, humanoid.leftFootTarget);
                CopyTarget(thisHumanoid.rightFootTarget, humanoid.rightFootTarget);
            }
        }
    }

    private void CopyTarget(HumanoidTarget sourceTarget, HumanoidTarget target) {
        if (mirror) {
            Vector3 localPosition = sourceTarget.humanoid.transform.InverseTransformPoint(sourceTarget.transform.position);
            target.transform.position = target.humanoid.transform.TransformPoint(MirrorX(localPosition));

            Quaternion localRotation = Quaternion.Inverse(sourceTarget.humanoid.transform.rotation) * sourceTarget.transform.rotation;
            target.transform.rotation = target.humanoid.transform.rotation * MirrorX(localRotation);
        } else {
            Vector3 localPosition = sourceTarget.humanoid.transform.InverseTransformPoint(sourceTarget.transform.position);
            target.transform.position = target.humanoid.transform.TransformPoint(localPosition);

            Quaternion localRotation = Quaternion.Inverse(sourceTarget.humanoid.transform.rotation) * sourceTarget.transform.rotation;
            target.transform.rotation = target.humanoid.transform.rotation * localRotation;
        }
    }

    private Vector3 MirrorX(Vector3 position) {
        return new Vector3(position.x, -position.y, position.z);
    }

    private Quaternion MirrorX(Quaternion rotation) {
        return new Quaternion(rotation.x * -1.0f, rotation.y, rotation.z, rotation.w * -1.0f);
    }
}
