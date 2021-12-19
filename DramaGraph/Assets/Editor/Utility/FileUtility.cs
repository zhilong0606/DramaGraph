using System;
using System.IO;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public static class FileUtility
{
    public static bool WriteToDisk<T>(string path, T data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        CheckoutIfValid(path);

        try
        {
            File.WriteAllText(path, EditorJsonUtility.ToJson(data, true));
        }
        catch (Exception e)
        {
            if (e.GetBaseException() is UnauthorizedAccessException &&
                (File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                FileInfo fileInfo = new FileInfo(path);
                fileInfo.IsReadOnly = false;
                File.WriteAllText(path, EditorJsonUtility.ToJson(data, true));
                return true;
            }
            Debug.LogException(e);
            return false;
        }
        return true;
    }

    static void CheckoutIfValid(string path)
    {
        if (Provider.enabled && Provider.isActive)
        {
            var asset = Provider.GetAssetByPath(path);
            if (asset != null)
            {
                if (!Provider.IsOpenForEdit(asset))
                {
                    var task = Provider.Checkout(asset, CheckoutMode.Asset);
                    task.Wait();

                    if (!task.success)
                        Debug.Log(task.text + " " + task.resultCode);
                }
            }
        }
    }
}
