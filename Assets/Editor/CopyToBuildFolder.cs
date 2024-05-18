using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;

public class CopyToBuildFolder : Editor, IPostprocessBuildWithReport
{
    public int callbackOrder => 0;

    public static readonly string[] filesToCopy = { "SpoutWinCapture.exe" };

    public void OnPostprocessBuild(BuildReport report)
    {
        string dir = Path.GetDirectoryName(report.summary.outputPath);

        foreach (string srcpath in filesToCopy)
        {
            string filename = Path.GetFileName(srcpath);
            string destPath = Path.Combine(dir, filename);

            try
            {
                FileUtil.CopyFileOrDirectory(srcpath, destPath);
            }
            catch (IOException e)
            {
                Debug.LogWarning(string.Format("Error copying {0}: {1}", srcpath, e.Message));
            }
        }

        Debug.Log(string.Format("Output Dir: '{0}'", dir));
    }
}
