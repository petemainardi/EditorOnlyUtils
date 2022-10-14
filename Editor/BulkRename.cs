using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

namespace EditorOnlyUtils
{
	/// ===========================================================================================
	/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	/// ===========================================================================================
	/**
	 *  Add a menu item for renaming selected objects.
	 */
	/// ===========================================================================================
	/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	/// ===========================================================================================
	public static class BulkRename
    {
        [MenuItem("GameObject/~~ Bulk Rename", true)]
        public static bool Validate() => Selection.activeTransform != null;


		/// <summary>
		/// Rename selected objects as if they were duplicates of the first selected object.
		/// </summary>
        [MenuItem("GameObject/~~ Bulk Rename", false, 11)]
        public static void Rename()
		{
			UnityEngine.Object[] objects = Selection.objects;

			for (int i = 1; i < objects.Length; i++)
			{
				objects[i].name = $"{objects[0].name} ({i})";
			}
		}
    }
	/// ===========================================================================================
	/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	/// ===========================================================================================
}