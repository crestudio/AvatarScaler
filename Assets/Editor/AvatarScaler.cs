#if UNITY_EDITOR
using System.Linq;

using UnityEditor;
using UnityEngine;

using VRC.SDKBase;

/*
 * VRSuya AvatarScaler
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.avatarscaler {

	public class AvatarScaler : MonoBehaviour {

		private readonly static float Kikyo1mEyePosition = 0.892182f;

		private static int UndoGroupIndex;

		/// <summary>�ƹ�Ÿ�� Ű�� 145cm�� ����ϴ�.</summary>
		[MenuItem("Tools/VRSuya/AvatarScaler/145cm", priority = 1200)]
		public static void ScaleAvatar145cm() {
			ScaleAvatar(145);
			return;
		}

		/// <summary>�ƹ�Ÿ�� Ű�� 160cm�� ����ϴ�.</summary>
		[MenuItem("Tools/VRSuya/AvatarScaler/160cm", priority = 1201)]
		public static void ScaleAvatar160cm() {
			ScaleAvatar(160);
			return;
		}

		/// <summary>������ Ű�� ��ǥ�� �ƹ�Ÿ �������� �����մϴ�.</summary>
		private static void ScaleAvatar(int TargetHeight) {
			if (GetVRCAvatar()) {
				VRC_AvatarDescriptor AvatarDescriptor = GetVRCAvatar();
				GameObject AvatarObject = AvatarDescriptor.gameObject;
				Vector3 AvatarViewPosition = AvatarDescriptor.ViewPosition;
				float TargetEyeHeight = Kikyo1mEyePosition * TargetHeight;
				float TargetAvatarScale = TargetEyeHeight / AvatarViewPosition.y;
				Undo.IncrementCurrentGroup();
				Undo.SetCurrentGroupName("VRSuya AvatarScaler");
				UndoGroupIndex = Undo.GetCurrentGroup();
				ScaleAvatarTransform(AvatarObject, TargetAvatarScale);
				ScaleAvatarViewPosition(AvatarDescriptor, TargetAvatarScale);
				SceneView.RepaintAll();
				Debug.Log("[AvatarScaler] " + AvatarObject.name + " �ƹ�Ÿ Ű�� " + TargetHeight + "cm���� ���߾����ϴ�!");
			}
			return;
		}

		/// <summary>�ƹ�Ÿ�� �������� �����մϴ�.</summary>
		private static void ScaleAvatarTransform(GameObject TargetAvatar, float TargetScale) {
			Transform TargetAvatarTransform = TargetAvatar.transform;
			Undo.RecordObject(TargetAvatarTransform, "Changed Avatar Transform");
			TargetAvatarTransform.localScale = TargetAvatarTransform.localScale * TargetScale;
			EditorUtility.SetDirty(TargetAvatarTransform);
			Undo.CollapseUndoOperations(UndoGroupIndex);
		}

		/// <summary>�ƹ�Ÿ�� �� �������� �����մϴ�.</summary>
		private static void ScaleAvatarViewPosition(VRC_AvatarDescriptor TargetAvatarDescritor, float TargetScale) {
			Undo.RecordObject(TargetAvatarDescritor, "Changed Avatar View Position");
			TargetAvatarDescritor.ViewPosition = TargetAvatarDescritor.ViewPosition * TargetScale;
			EditorUtility.SetDirty(TargetAvatarDescritor);
			Undo.CollapseUndoOperations(UndoGroupIndex);
		}

		/// <summary>Scene���� ���ǿ� �´� VRC AvatarDescriptor ������Ʈ �ƹ�Ÿ 1���� ��ȯ�մϴ�.</summary>
		/// <returns>���ǿ� �´� VRC �ƹ�Ÿ</returns>
		private static VRC_AvatarDescriptor GetVRCAvatar() {
			VRC_AvatarDescriptor TargetAvatarDescriptor = GetAvatarDescriptorFromVRCSDKBuilder();
			if (!TargetAvatarDescriptor) TargetAvatarDescriptor = GetAvatarDescriptorFromSelection();
			if (!TargetAvatarDescriptor) TargetAvatarDescriptor = GetAvatarDescriptorFromVRCTool();
			return TargetAvatarDescriptor;
		}

		/// <summary>VRCSDK Builder���� Ȱ��ȭ ������ VRC �ƹ�Ÿ�� ��ȯ�մϴ�.</summary>
		/// <returns>VRCSDK Builder���� Ȱ��ȭ ������ VRC �ƹ�Ÿ</returns>
		private static VRC_AvatarDescriptor GetAvatarDescriptorFromVRCSDKBuilder() {
			return null;
		}

		/// <summary>Unity ���̾��Ű���� ������ GameObject �߿��� VRC AvatarDescriptor ������Ʈ�� �����ϴ� �ƹ�Ÿ�� 1���� ��ȯ�մϴ�.</summary>
		/// <returns>���� ���� VRC �ƹ�Ÿ</returns>
		private static VRC_AvatarDescriptor GetAvatarDescriptorFromSelection() {
			GameObject[] SelectedGameObjects = Selection.gameObjects;
			if (SelectedGameObjects.Length == 1) {
				VRC_AvatarDescriptor SelectedVRCAvatarDescriptor = SelectedGameObjects[0].GetComponent<VRC_AvatarDescriptor>();
				if (SelectedVRCAvatarDescriptor) {
					return SelectedVRCAvatarDescriptor;
				} else {
					return null;
				}
			} else if (SelectedGameObjects.Length > 1) {
				VRC_AvatarDescriptor SelectedVRCAvatarDescriptor = SelectedGameObjects
					.Where(SelectedGameObject => SelectedGameObject.activeInHierarchy == true)
					.Select(SelectedGameObject => SelectedGameObject.GetComponent<VRC_AvatarDescriptor>()).ToArray()[0];
				if (SelectedVRCAvatarDescriptor) {
					return SelectedVRCAvatarDescriptor;
				} else {
					return null;
				}
			} else {
				return null;
			}
		}

		/// <summary>Scene���� Ȱ��ȭ ������ VRC AvatarDescriptor ������Ʈ�� �����ϴ� �ƹ�Ÿ�� 1���� ��ȯ�մϴ�.</summary>
		/// <returns>Scene���� Ȱ��ȭ ������ VRC �ƹ�Ÿ</returns>
		private static VRC_AvatarDescriptor GetAvatarDescriptorFromVRCTool() {
			VRC_AvatarDescriptor[] AllVRCAvatarDescriptor = VRC.Tools.FindSceneObjectsOfTypeAll<VRC_AvatarDescriptor>().ToArray();
			if (AllVRCAvatarDescriptor.Length > 0) {
				return AllVRCAvatarDescriptor.Where(Avatar => Avatar.gameObject.activeInHierarchy).ToArray()[0];
			} else {
				return null;
			}
		}
	}
}
#endif