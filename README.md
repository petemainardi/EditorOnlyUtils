# EditorOnlyUtils
A collection of tools to extend the Unity editor functionality.

The goal of this package is to provide extra tools for the editor without becoming a dependency of your project. Import it when you need it, delete it when you're done, and there should be no effect on your codebase*, just improvements to your workflow.

<p><sub>*apart from the Packages/manifest.json that is, that's unavoidable. Just make sure to not commit it, or remove this package before you do.</sub></p>

<br />
Add to your project with the Package Manager using the git url:
<blockquote>https://github.com/petemainardi/EditorOnlyUtils.git</blockquote>

Or by adding the following to your project's Packages/manifest.json file:
<blockquote>"com.editoronly.utils": "https://github.com/petemainardi/EditorOnlyUtils.git"</blockquote>

## Summary of Functionality

### Isolated Transform Reset
Menu item visible when a gameobject is selected, resets transform attributes so that they are locally zeroed out without affecting the global transform attributes of the transform's children.

### Bulk Rename
Menu item visible when a gameobject is selected, allowing the following options for renaming the selected objects:
- Rename selected objects as if they were duplicates of the first selected object.
- Remove Unity-enumerated suffix at the end of each selected object's name.
