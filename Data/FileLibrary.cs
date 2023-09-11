using System;

public static class FileLibrary
{
    /// <summary>
    /// Crea un fichero vacio fichero vacio
    /// </summary>
    public static void CreateEmptyFile(string fullPath)
    {
        if (!System.IO.File.Exists(fullPath))
        {
            using (System.IO.File.Create(fullPath)) { }
        }
    }


    /// <summary>
    /// Crear un directorio vacio
    /// </summary>
    public static void CreateEmptyDirectory(string fullPath)
    {
        if (!System.IO.Directory.Exists(fullPath))
        {
            System.IO.Directory.CreateDirectory(fullPath);
        }
    }


    /// <summary>
    /// Borrar un archivo
    /// </summary>
    public static void DeleteFile(string fullPath)
    {
        if (System.IO.File.Exists(fullPath))
        {
            System.IO.FileInfo info = new System.IO.FileInfo(fullPath);
            info.Attributes = System.IO.FileAttributes.Normal;
            System.IO.File.Delete(fullPath);
        }
    }


    /// <summary>
    /// Borrar un directorio y su contenido
    /// </summary>
    public static void DeleteFolder(string fullPath)
    {
        if (System.IO.Directory.Exists(fullPath))
        {
            System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(fullPath)
            {
                Attributes = System.IO.FileAttributes.Normal
            };

            foreach (var info in directory.GetFileSystemInfos("*", System.IO.SearchOption.AllDirectories))
            {
                System.IO.FileInfo fInfo = info as System.IO.FileInfo;
                if (fInfo != null) info.Attributes = System.IO.FileAttributes.Normal;
            }
            System.Threading.Thread.Sleep(100);
            directory.Delete(true);
        }

    }


    /// <summary>
    /// Borra el contenido de un directorio
    /// </summary>
    public static void DeleteFolderContent(string fullPath)
    {
        DeleteFolder(fullPath);
        CreateEmptyDirectory(fullPath);
    }


    /// <summary>
    /// Copiar archivo
    /// </summary>
    public static void CopyFile(string origPath, string destPath, bool overwrite)
    {
        try
        {
            if (System.IO.Path.GetExtension(destPath) == "")
            {
                destPath = System.IO.Path.Combine(destPath, System.IO.Path.GetFileName(origPath));
            }
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(destPath)))
            {
                CreateEmptyDirectory(System.IO.Path.GetDirectoryName(destPath));
            }
            if (!System.IO.File.Exists(destPath))
            {
                System.IO.File.Copy(origPath, destPath, true);
            }
            else
            {
                if (overwrite == true)
                {
                    DeleteFile(destPath);
                    System.IO.File.Copy(origPath, destPath, true);
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }


    /// <summary>
    /// Copiar el contenido de un directorio
    /// </summary>
    private static void CopyDirectoryContent(string origPath, string destPath, bool overwrite)
    {
        if (System.IO.Directory.Exists(origPath))
        {
            foreach (string dirPath in System.IO.Directory.GetDirectories(origPath, "*", System.IO.SearchOption.AllDirectories))
            {
                CreateEmptyDirectory(dirPath.Replace(origPath, destPath));
            }

            foreach (string newPath in System.IO.Directory.GetFiles(origPath, "*.*", System.IO.SearchOption.AllDirectories))
            {
                CopyFile(newPath, newPath.Replace(origPath, destPath), overwrite);
            }
        }
    }


    /// <summary>
    /// Copiar un directorio y su contenido
    /// </summary>
    public static void CopyDirectory(string origPath, string destPath, bool replace)
    {
        if (replace == true)
        {
            DeleteFolder(destPath);
            CreateEmptyDirectory(destPath);
            CopyDirectoryContent(origPath, destPath, true);
        }
        else
        {
            CopyDirectoryContent(origPath, destPath, true);
        }
    }

}
