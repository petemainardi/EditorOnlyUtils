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
        [MenuItem("GameObject/~~ Bulk Rename/Rename to match first", true)]
        [MenuItem("GameObject/~~ Bulk Rename/Clear enum suffix", true)]
        public static bool Validate() => Selection.activeTransform != null;


		/// <summary>
		/// Rename selected objects as if they were duplicates of the first selected object.
		/// </summary>
		/// <remarks>
		/// Attempts to maintain Unity's convention for incrementing the enumeration of the initial object.
		/// </remarks>
        [MenuItem("GameObject/~~ Bulk Rename/Rename to match first", false, 11)]
        public static void RenameEnumerated()
		{
			UnityEngine.Object[] objects = Selection.objects;
			Undo.RecordObjects(objects, "Bulk Rename");

			int offset = 0;
            string firstName = objects[offset].name;
			Match match = new Regex(@"\s\([0-9]+\)(?!.+)").Match(firstName); // ends with " (#)"
            if (match.Success)
            {
				int numStart = firstName.LastIndexOf('(');
				string number = firstName.Remove(firstName.Length - 1).Substring(1 + numStart);
				offset = int.Parse(number);

				firstName = firstName.Substring(0, numStart);
            }

            for (int i = 1; i < objects.Length; i++)
			{
				objects[i].name = $"{firstName.TrimEnd()} ({offset + i})";
			}
		}

		/// <summary>
		/// Remove Unity-enumerated suffix at the end of an object's name.
		/// </summary>
        [MenuItem("GameObject/~~ Bulk Rename/Clear enum suffix", false, 11)]
        public static void ClearEnumerationSuffix()
        {
            UnityEngine.Object[] objects = Selection.objects;
            Undo.RecordObjects(objects, "Bulk Rename");

            for (int i = 0; i < objects.Length; i++)
            {
                Match match = new Regex(@"\s\([0-9]+\)(?!.+)").Match(objects[i].name); // ends with " (#)"
                if (match.Success)
                {
					objects[i].name = objects[i].name.Substring(0, objects[i].name.LastIndexOf('(')).TrimEnd();
                }
            }
        }
    }
	/// ===========================================================================================
	/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	/// ===========================================================================================
}