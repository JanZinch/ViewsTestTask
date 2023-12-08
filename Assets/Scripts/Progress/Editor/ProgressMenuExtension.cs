using UnityEditor;

namespace Progress.Editor
{
    public static class ProgressMenuExtension
    {
        [MenuItem("Tools/Progress/Clear")]
        public static void ClearProgress()
        {
            new ProgressDataAdapter().ClearProgress();
        }
    }
}