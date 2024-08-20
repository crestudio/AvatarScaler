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

		/// <summary>아바타의 키를 145cm에 맞춥니다.</summary>
		[MenuItem("Tools/VRSuya/AvatarScaler/145cm", priority = 1200)]
		public static void ScaleAvatar145cm() {
			ScaleAvatar(145);
			return;
		}

		/// <summary>아바타의 키를 160cm에 맞춥니다.</summary>
		[MenuItem("Tools/VRSuya/AvatarScaler/160cm", priority = 1201)]
		public static void ScaleAvatar160cm() {
			ScaleAvatar(160);
			return;
		}

		/// <summary>지정된 키를 목표로 아바타 스케일을 변경합니다.</summary>
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
				Debug.Log("[AvatarScaler] " + AvatarObject.name + " 아바타 키를 " + TargetHeight + "cm으로 맞추었습니다!");
			}
			return;
		}

		/// <summary>아바타의 스케일을 변경합니다.</summary>
		private static void ScaleAvatarTransform(GameObject TargetAvatar, float TargetScale) {
			Transform TargetAvatarTransform = TargetAvatar.transform;
			Undo.RecordObject(TargetAvatarTransform, "Changed Avatar Transform");
			TargetAvatarTransform.localScale = TargetAvatarTransform.localScale * TargetScale;
			EditorUtility.SetDirty(TargetAvatarTransform);
			Undo.CollapseUndoOperations(UndoGroupIndex);
		}

		/// <summary>아바타의 뷰 포지션을 변경합니다.</summary>
		private static void ScaleAvatarViewPosition(VRC_AvatarDescriptor TargetAvatarDescritor, float TargetScale) {
			Undo.RecordObject(TargetAvatarDescritor, "Changed Avatar View Position");
			TargetAvatarDescritor.ViewPosition = TargetAvatarDescritor.ViewPosition * TargetScale;
			EditorUtility.SetDirty(TargetAvatarDescritor);
			Undo.CollapseUndoOperations(UndoGroupIndex);
		}

		/// <summary>Scene에서 조건에 맞는 VRC AvatarDescriptor 컴포넌트 아바타 1개를 반환합니다.</summary>
		/// <returns>조건에 맞는 VRC 아바타</returns>
		private static VRC_AvatarDescriptor GetVRCAvatar() {
			VRC_AvatarDescriptor TargetAvatarDescriptor = GetAvatarDescriptorFromVRCSDKBuilder();
			if (!TargetAvatarDescriptor) TargetAvatarDescriptor = GetAvatarDescriptorFromSelection();
			if (!TargetAvatarDescriptor) TargetAvatarDescriptor = GetAvatarDescriptorFromVRCTool();
			return TargetAvatarDescriptor;
		}

		/// <summary>VRCSDK Builder에서 활성화 상태인 VRC 아바타를 반환합니다.</summary>
		/// <returns>VRCSDK Builder에서 활성화 상태인 VRC 아바타</returns>
		private static VRC_AvatarDescriptor GetAvatarDescriptorFromVRCSDKBuilder() {
			return null;
		}

		/// <summary>Unity 하이어라키에서 선택한 GameObject 중에서 VRC AvatarDescriptor 컴포넌트가 존재하는 아바타를 1개를 반환합니다.</summary>
		/// <returns>선택 중인 VRC 아바타</returns>
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

		/// <summary>Scene에서 활성화 상태인 VRC AvatarDescriptor 컴포넌트가 존재하는 아바타를 1개를 반환합니다.</summary>
		/// <returns>Scene에서 활성화 상태인 VRC 아바타</returns>
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