using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EditorOnlyUtils
{
	/// ===========================================================================================
	/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	/// ===========================================================================================
	/**
	 *  Add menu items for resetting transform attributes so that they are locally zeroed out
	 *  without affecting the global transform attributes of the transform's children.
	 */
	/// ===========================================================================================
	/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	/// ===========================================================================================
	public static class BakeTransforms
    {
        [MenuItem("GameObject/~~ Isolated Transform Reset/Position", true)]
        [MenuItem("GameObject/~~ Isolated Transform Reset/Rotation", true)]
        [MenuItem("GameObject/~~ Isolated Transform Reset/Scale", true)]
        [MenuItem("GameObject/~~ Isolated Transform Reset/All", true)]
		public static bool ValidateBakeTransforms() => Selection.activeTransform != null;


        [MenuItem("GameObject/~~ Isolated Transform Reset/All", false, 10)]
        public static void ResetAll() => Reset(true, true, true, "All");

        [MenuItem("GameObject/~~ Isolated Transform Reset/Position", false, 10)]
        public static void ResetPosition() => Reset(true, false, false, "Position");


        [MenuItem("GameObject/~~ Isolated Transform Reset/Rotation", false, 10)]
        public static void ResetRotation() => Reset(false, true, false, "Rotation");


        [MenuItem("GameObject/~~ Isolated Transform Reset/Scale", false, 10)]
        public static void ResetScale() => Reset(false, false, true, "Scale");


        public static void Reset(bool pos, bool rot, bool scl, string name)
        {
            name = $"Isolated Reset {name}";

            Transform t = Selection.activeTransform;
            Undo.RecordObject(t.transform, name);

            Dictionary<Transform, (Vector3, Quaternion, Vector3)> transforms = new Dictionary<Transform, (Vector3, Quaternion, Vector3)>();
            for (int i = 0; i < t.childCount; i++)
            {
                Transform child = t.GetChild(i);
                transforms.Add(child, (child.position, child.rotation, child.localScale));
                Undo.RecordObject(child, name);
            }


            if (pos)
                t.position = Vector3.zero;

            if (rot)
                t.localRotation = Quaternion.identity;

            Vector3 scale = t.localScale;
            if (scl)
                t.localScale = Vector3.one;
            else
                scale = Vector3.one;


            foreach (Transform child in transforms.Keys)
            {
                child.position = transforms[child].Item1;
                child.rotation = transforms[child].Item2;
                child.localScale = Vector3.Scale(transforms[child].Item3, scale);
            }
        }

	}
	/// ===========================================================================================
	/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	/// ===========================================================================================
}