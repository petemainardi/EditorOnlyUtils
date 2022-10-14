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
		public static bool Validate() => Selection.activeTransform != null;


        [MenuItem("GameObject/~~ Isolated Transform Reset/All", false, 10)]
        public static void ResetAll() => Reset(true, true, true, "All");

        [MenuItem("GameObject/~~ Isolated Transform Reset/Position", false, 10)]
        public static void ResetPosition() => Reset(true, false, false, "Position");


        [MenuItem("GameObject/~~ Isolated Transform Reset/Rotation", false, 10)]
        public static void ResetRotation() => Reset(false, true, false, "Rotation");


        [MenuItem("GameObject/~~ Isolated Transform Reset/Scale", false, 10)]
        public static void ResetScale() => Reset(false, false, true, "Scale");


        /// <summary>
        /// Locally clear transform attributes without affecting the global attributes of child transforms.
        /// </summary>
        /// <param name="pos">Wether to clear position</param>
        /// <param name="rot">Whether to clear rotation</param>
        /// <param name="scale">Whether to clear scale</param>
        /// <param name="attrName">Name(s) of the attribute(s) to be reset, displayed as "Isolated Reset {name}"</param>
        public static void Reset(bool pos, bool rot, bool scale, string attrName)
        {
            attrName = $"Isolated Reset {attrName}";

            Transform t = Selection.activeTransform;
            Undo.RecordObject(t.transform, attrName);

            Dictionary<Transform, (Vector3, Quaternion, Vector3)> transforms = new Dictionary<Transform, (Vector3, Quaternion, Vector3)>();
            for (int i = 0; i < t.childCount; i++)
            {
                Transform child = t.GetChild(i);
                transforms.Add(child, (child.position, child.rotation, child.localScale));
                Undo.RecordObject(child, attrName);
            }


            if (pos)
                t.position = Vector3.zero;

            if (rot)
                t.localRotation = Quaternion.identity;

            Vector3 scl = t.localScale;
            if (scale)
                t.localScale = Vector3.one;
            else
                scl = Vector3.one;


            foreach (Transform child in transforms.Keys)
            {
                child.position = transforms[child].Item1;
                child.rotation = transforms[child].Item2;
                child.localScale = Vector3.Scale(transforms[child].Item3, scl);
            }
        }

	}
	/// ===========================================================================================
	/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	/// ===========================================================================================
}