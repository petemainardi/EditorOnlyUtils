using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MainArtery.Utilities.Editor
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
        [MenuItem("GameObject/Isolated Transform Reset/Position", true)]
        [MenuItem("GameObject/Isolated Transform Reset/Rotation", true)]
        [MenuItem("GameObject/Isolated Transform Reset/Scale", true)]
        [MenuItem("GameObject/Isolated Transform Reset/All", true)]
		public static bool ValidateBakeTransforms() => Selection.activeTransform != null;


        [MenuItem("GameObject/Isolated Transform Reset/Position", false, 11)]
        public static void ResetPosition() => Reset(true, false, false);


        [MenuItem("GameObject/Isolated Transform Reset/Rotation", false, 11)]
        public static void ResetRotation() => Reset(false, true, false);


        [MenuItem("GameObject/Isolated Transform Reset/Scale", false, 11)]
        public static void ResetScale() => Reset(false, false, true);


        [MenuItem("GameObject/Isolated Transform Reset/All", false, 10)]
        public static void ResetAll() => Reset(true, true, true);


        public static void Reset(bool pos, bool rot, bool scl)
        {
            GameObject g = new GameObject();
            Undo.RegisterCreatedObjectUndo(g, "Isolated Reset All");

            Transform t = Selection.activeTransform;
            Transform[] children = new Transform[t.childCount];

            for (int i = 0; i < children.Length; i++)
            {
                Transform child = t.GetChild(0);
                Undo.SetTransformParent(child, g.transform, true, "Isolated Reset All");
                children[i] = child;
            }

            Undo.RecordObject(t.transform, "Isolated Reset All");
            if (pos)
                t.localPosition = Vector3.zero;
            if (rot)
                t.localRotation = Quaternion.identity;
            if (scl)
                t.localScale = Vector3.one;

            for (int i = 0; i < children.Length; i++)
            {
                Undo.SetTransformParent(children[i], t, true, "Isolated Reset All");
                Undo.RecordObject(children[i].transform, "Isolated Reset All");
                children[i].SetSiblingIndex(i);
            }

            Undo.DestroyObjectImmediate(g);
        }

	}
	/// ===========================================================================================
	/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	/// ===========================================================================================
}