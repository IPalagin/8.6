﻿// See https://aka.ms/new-console-template for more information
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml.Linq;

string folderPath = @"C:\Users\igor_\OneDrive\Рабочий стол\TrashFolder";

if (Directory.Exists(folderPath))
{
    DirectoryInfo dir = new DirectoryInfo(folderPath);
    DateTime currentTime = DateTime.Now;
    DeleteFilesAndFolders(dir, currentTime);
}
else
{
    Console.WriteLine("Папка не существует: " + folderPath);
}

    static void DeleteFilesAndFolders(DirectoryInfo dir, DateTime currentTime)
{
    foreach (FileInfo file in dir.GetFiles())
    {
        TimeSpan timeLastAccess = currentTime - file.LastAccessTime;
        if (timeLastAccess.TotalMinutes > 30)
        {
            Console.WriteLine("Удаляем файл: " + file.Name);
            try
            {
                file.Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при удалении файла: " + ex.Message);
            }
        }
    }

    foreach (DirectoryInfo subDir in dir.GetDirectories())
    {
        DeleteFilesAndFolders(subDir, currentTime);
        if (subDir.GetFiles().Length == 0 && subDir.GetDirectories().Length == 0)
        {
            Console.WriteLine("Удаляем пустую папку: " + subDir.Name);
            try
            {
                subDir.Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при удалении папки: " + ex.Message);
            }
        }
    }
}