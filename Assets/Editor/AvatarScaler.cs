#if UNITY_EDITOR
using System.Collections.Generic;
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

		public enum Avatar {
			Chiffon,
			Grus,
			Karin,
			Kikyo,
			Leefa,
			Lime,
			Mamehinata,
			Manuka,
			Maya,
			Minase,
			Moe,
			Selestia,
			Shinra,
			Sio
		}

		private readonly static Dictionary<Avatar, float> AvatarEyeHeights = new Dictionary<Avatar, float>() {
			{ Avatar.Chiffon, 0.880152f },
			{ Avatar.Grus, 0.892328f },
			{ Avatar.Karin, 0.87956f },
			{ Avatar.Kikyo, 0.892182f },
			{ Avatar.Leefa, 0.886995f },
			{ Avatar.Lime, 0.89622f },
			{ Avatar.Mamehinata, 0.8167276f },
			{ Avatar.Manuka, 0.8817998f },
			{ Avatar.Maya, 0.8845845f },
			{ Avatar.Minase, 0.91609f },
			{ Avatar.Moe, 0.897036f },
			{ Avatar.Selestia, 0.8838221f },
			{ Avatar.Shinra, 0.900882f },
			{ Avatar.Sio, 0.9020135f }
		};

		public static Avatar CurrentAvatarType = Avatar.Kikyo;
		private static int UndoGroupIndex;

		/// <summary>아바타를 지정된 타입에 맞춥니다.</summary>
		[MenuItem("Tools/VRSuya/AvatarScaler/Avatar/Chiffon", priority = 1100)]
		public static void SetAvatarTypeChiffon() {
			CurrentAvatarType = Avatar.Chiffon;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/Avatar/Grus", priority = 1101)]
		public static void SetAvatarTypeGrus() {
			CurrentAvatarType = Avatar.Grus;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/Avatar/Karin", priority = 1102)]
		public static void SetAvatarTypeKarin() {
			CurrentAvatarType = Avatar.Karin;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/Avatar/Kikyo", priority = 1103)]
		public static void SetAvatarTypeKikyo() {
			CurrentAvatarType = Avatar.Kikyo;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/Avatar/Leefa", priority = 1104)]
		public static void SetAvatarTypeLeefa() {
			CurrentAvatarType = Avatar.Leefa;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/Avatar/Lime", priority = 1105)]
		public static void SetAvatarTypeLime() {
			CurrentAvatarType = Avatar.Lime;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/Avatar/Mamehinata", priority = 1106)]
		public static void SetAvatarTypeMamehinata() {
			CurrentAvatarType = Avatar.Mamehinata;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/Avatar/Manuka", priority = 1107)]
		public static void SetAvatarTypeManuka() {
			CurrentAvatarType = Avatar.Manuka;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/Avatar/Maya", priority = 1108)]
		public static void SetAvatarTypeMaya() {
			CurrentAvatarType = Avatar.Maya;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/Avatar/Minase", priority = 1109)]
		public static void SetAvatarTypeMinase() {
			CurrentAvatarType = Avatar.Minase;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/Avatar/Moe", priority = 1110)]
		public static void SetAvatarTypeMoe() {
			CurrentAvatarType = Avatar.Moe;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/Avatar/Selestia", priority = 1111)]
		public static void SetAvatarTypeSelestia() {
			CurrentAvatarType = Avatar.Selestia;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/Avatar/Shinra", priority = 1112)]
		public static void SetAvatarTypeShinra() {
			CurrentAvatarType = Avatar.Shinra;
			CheckAvatarMenu();
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/Avatar/Sio", priority = 1113)]
		public static void SetAvatarTypeSio() {
			CurrentAvatarType = Avatar.Sio;
			CheckAvatarMenu();
			return;
		}

		/// <summary>아바타의 키를 지정된 키에 맞춥니다.</summary>
		[MenuItem("Tools/VRSuya/AvatarScaler/100cm", priority = 1200)]
		public static void ScaleAvatar100cm() {
			ScaleAvatar(100);
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/110cm", priority = 1201)]
		public static void ScaleAvatar110cm() {
			ScaleAvatar(110);
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/120cm", priority = 1202)]
		public static void ScaleAvatar120cm() {
			ScaleAvatar(120);
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/130cm", priority = 1203)]
		public static void ScaleAvatar130cm() {
			ScaleAvatar(130);
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/140cm", priority = 1204)]
		public static void ScaleAvatar140cm() {
			ScaleAvatar(140);
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/150cm", priority = 1205)]
		public static void ScaleAvatar150cm() {
			ScaleAvatar(150);
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/160cm", priority = 1206)]
		public static void ScaleAvatar160cm() {
			ScaleAvatar(160);
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/170cm", priority = 1207)]
		public static void ScaleAvatar170cm() {
			ScaleAvatar(170);
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/180cm", priority = 1208)]
		public static void ScaleAvatar180cm() {
			ScaleAvatar(180);
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/190cm", priority = 1209)]
		public static void ScaleAvatar190cm() {
			ScaleAvatar(190);
			return;
		}

		[MenuItem("Tools/VRSuya/AvatarScaler/200cm", priority = 1210)]
		public static void ScaleAvatar200cm() {
			ScaleAvatar(200);
			return;
		}

		/// <summary>아바타 메뉴의 변수 상태를 체크합니다.</summary>
		private static void CheckAvatarMenu() {
			Menu.SetChecked("Tools/VRSuya/AvatarScaler/Avatar/Chiffon", CurrentAvatarType == Avatar.Chiffon);
			Menu.SetChecked("Tools/VRSuya/AvatarScaler/Avatar/Grus", CurrentAvatarType == Avatar.Grus);
			Menu.SetChecked("Tools/VRSuya/AvatarScaler/Avatar/Karin", CurrentAvatarType == Avatar.Karin);
			Menu.SetChecked("Tools/VRSuya/AvatarScaler/Avatar/Kikyo", CurrentAvatarType == Avatar.Kikyo);
			Menu.SetChecked("Tools/VRSuya/AvatarScaler/Avatar/Leefa", CurrentAvatarType == Avatar.Leefa);
			Menu.SetChecked("Tools/VRSuya/AvatarScaler/Avatar/Lime", CurrentAvatarType == Avatar.Lime);
			Menu.SetChecked("Tools/VRSuya/AvatarScaler/Avatar/Mamehinata", CurrentAvatarType == Avatar.Mamehinata);
			Menu.SetChecked("Tools/VRSuya/AvatarScaler/Avatar/Manuka", CurrentAvatarType == Avatar.Manuka);
			Menu.SetChecked("Tools/VRSuya/AvatarScaler/Avatar/Maya", CurrentAvatarType == Avatar.Maya);
			Menu.SetChecked("Tools/VRSuya/AvatarScaler/Avatar/Minase", CurrentAvatarType == Avatar.Minase);
			Menu.SetChecked("Tools/VRSuya/AvatarScaler/Avatar/Moe", CurrentAvatarType == Avatar.Moe);
			Menu.SetChecked("Tools/VRSuya/AvatarScaler/Avatar/Selestia", CurrentAvatarType == Avatar.Selestia);
			Menu.SetChecked("Tools/VRSuya/AvatarScaler/Avatar/Shinra", CurrentAvatarType == Avatar.Shinra);
			Menu.SetChecked("Tools/VRSuya/AvatarScaler/Avatar/Sio", CurrentAvatarType == Avatar.Sio);
			return;
		}

		/// <summary>지정된 키를 목표로 아바타 스케일을 변경합니다.</summary>
		private static void ScaleAvatar(int TargetHeight) {
			if (GetVRCAvatar().Length > 0) {
				foreach (VRC_AvatarDescriptor TargetAvatarDescriptor in GetVRCAvatar()) {
					VRC_AvatarDescriptor AvatarDescriptor = TargetAvatarDescriptor;
					GameObject AvatarObject = AvatarDescriptor.gameObject;
					Vector3 AvatarViewPosition = AvatarDescriptor.ViewPosition;
					float TargetEyeHeight = AvatarEyeHeights[CurrentAvatarType] * TargetHeight / 100;
					float TargetAvatarScale = TargetEyeHeight / AvatarViewPosition.y;
					Undo.IncrementCurrentGroup();
					Undo.SetCurrentGroupName("VRSuya AvatarScaler");
					UndoGroupIndex = Undo.GetCurrentGroup();
					ScaleAvatarTransform(AvatarObject, TargetAvatarScale);
					ScaleAvatarViewPosition(AvatarDescriptor, TargetAvatarScale);
					Debug.Log("[AvatarScaler] " + AvatarObject.name + " 아바타 키를 " + TargetHeight + "cm으로 맞추었습니다!");
				}
				SceneView.RepaintAll();
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
		private static VRC_AvatarDescriptor[] GetVRCAvatar() {
			VRC_AvatarDescriptor[] TargetAvatarDescriptors = GetAvatarDescriptorFromVRCSDKBuilder();
			if (TargetAvatarDescriptors.Length == 0) TargetAvatarDescriptors = GetAvatarDescriptorFromSelection();
			if (TargetAvatarDescriptors.Length == 0) TargetAvatarDescriptors = GetAvatarDescriptorFromVRCTool();
			return TargetAvatarDescriptors;
		}

		/// <summary>VRCSDK Builder에서 활성화 상태인 VRC 아바타를 반환합니다.</summary>
		/// <returns>VRCSDK Builder에서 활성화 상태인 VRC 아바타</returns>
		private static VRC_AvatarDescriptor[] GetAvatarDescriptorFromVRCSDKBuilder() {
			return new VRC_AvatarDescriptor[0];
		}

		/// <summary>Unity 하이어라키에서 선택한 GameObject 중에서 VRC AvatarDescriptor 컴포넌트가 존재하는 아바타를 1개를 반환합니다.</summary>
		/// <returns>선택 중인 VRC 아바타</returns>
		private static VRC_AvatarDescriptor[] GetAvatarDescriptorFromSelection() {
			GameObject[] SelectedGameObjects = Selection.gameObjects;
			if (SelectedGameObjects.Length == 1) {
				VRC_AvatarDescriptor SelectedVRCAvatarDescriptor = SelectedGameObjects[0].GetComponent<VRC_AvatarDescriptor>();
				if (SelectedVRCAvatarDescriptor) {
					return new VRC_AvatarDescriptor[] { SelectedVRCAvatarDescriptor };
				} else {
					return new VRC_AvatarDescriptor[0];
				}
			} else if (SelectedGameObjects.Length > 1) {
				VRC_AvatarDescriptor[] SelectedVRCAvatarDescriptor = SelectedGameObjects
					.Select(SelectedGameObject => SelectedGameObject.GetComponent<VRC_AvatarDescriptor>()).ToArray();
				return SelectedVRCAvatarDescriptor;
			} else {
				return new VRC_AvatarDescriptor[0];
			}
		}

		/// <summary>Scene에서 활성화 상태인 VRC AvatarDescriptor 컴포넌트가 존재하는 아바타를 1개를 반환합니다.</summary>
		/// <returns>Scene에서 활성화 상태인 VRC 아바타</returns>
		private static VRC_AvatarDescriptor[] GetAvatarDescriptorFromVRCTool() {
			VRC_AvatarDescriptor[] AllVRCAvatarDescriptor = VRC.Tools.FindSceneObjectsOfTypeAll<VRC_AvatarDescriptor>().ToArray();
			if (AllVRCAvatarDescriptor.Length > 0) {
				return AllVRCAvatarDescriptor.Where(Avatar => Avatar.gameObject.activeInHierarchy).ToArray();
			} else {
				return new VRC_AvatarDescriptor[0];
			}
		}
	}
}
#endif