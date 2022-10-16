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
	public static class IsolatedTransformReset
    {
        [Flags]
        public enum TransformAttrs
        {
            Position = 1 << 0,
            Rotation = 1 << 1,
            Scale    = 1 << 2,

            All = Position | Rotation | Scale
        }

        [MenuItem("GameObject/~~ Isolated Transform Reset/Position", true)]
        [MenuItem("GameObject/~~ Isolated Transform Reset/Rotation", true)]
        [MenuItem("GameObject/~~ Isolated Transform Reset/Scale", true)]
        [MenuItem("GameObject/~~ Isolated Transform Reset/All", true)]
		public static bool Validate() => Selection.activeTransform != null;


        [MenuItem("GameObject/~~ Isolated Transform Reset/All", false, 10)]
        public static void ResetAll() => Reset(TransformAttrs.All);

        [MenuItem("GameObject/~~ Isolated Transform Reset/Position", false, 10)]
        public static void ResetPosition() => Reset(TransformAttrs.Position);


        [MenuItem("GameObject/~~ Isolated Transform Reset/Rotation", false, 10)]
        public static void ResetRotation() => Reset(TransformAttrs.Rotation);


        [MenuItem("GameObject/~~ Isolated Transform Reset/Scale", false, 10)]
        public static void ResetScale() => Reset(TransformAttrs.Scale);


        /// <summary>
        /// Locally clear transform attributes without affecting the global attributes of child transforms.
        /// </summary>
        /// <param name="attrs">Which attribute(s) to reset</param>
        /// <param name="attrName">Name(s) of the attribute(s) to be reset, displayed as "Isolated Reset {name}"</param>
        public static void Reset(TransformAttrs attrs)
        {
            string attrName = $"Isolated Reset {attrs}";

            Transform t = Selection.activeTransform;
            Undo.RecordObject(t.transform, attrName);

            Dictionary<Transform, (Vector3 pos, Quaternion rot, Vector3 scale)> transforms =
                new Dictionary<Transform, (Vector3, Quaternion, Vector3)>();
            for (int i = 0; i < t.childCount; i++)
            {
                Transform child = t.GetChild(i);
                transforms.Add(child, (child.position, child.rotation, child.localScale));
                Undo.RecordObject(child, attrName);
            }


            if ((TransformAttrs.Position & attrs) == TransformAttrs.Position)
                t.position = Vector3.zero;

            if ((TransformAttrs.Rotation & attrs) == TransformAttrs.Rotation)
                t.localRotation = Quaternion.identity;

            Vector3 scl = t.localScale;
            if ((TransformAttrs.Scale & attrs) == TransformAttrs.Scale)
                t.localScale = Vector3.one;
            else
                scl = Vector3.one;


            foreach (Transform child in transforms.Keys)
            {
                child.position = transforms[child].pos;
                child.rotation = transforms[child].rot;
                child.localScale = Vector3.Scale(transforms[child].scale, scl);
            }
        }

	}
	/// ===========================================================================================
	/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	/// ===========================================================================================
}