# EditorOnlyUtils
A collection of tools to extend the Unity editor functionality.

This package avoids runtime extensions to allow you to avoid committing to it as a dependency for your project. Import it when you need it, delete it when you're done, and there should be no effect on your codebase, just improvements to your workflow.


Add to your project with the Package Manager using the git url:
https://github.com/petemainardi/EditorOnlyUtils.git

Or by adding the following to your project's Packages/manifest.json file:
<blockquote>
	<p>"com.editoronly.utils": "https://github.com/petemainardi/EditorOnlyUtils.git"</p>
</blockquote>

## Summary of Functionality

### Isolated Transform Reset
Menu item visible when a gameobject is selected, resets transform attributes so that they are locally zeroed out without affecting the global transform attributes of the transform's children.

### Bulk Rename
Menu item visible when a gameobject is selected, renames selected objects as if they were duplicates of the first selected object.