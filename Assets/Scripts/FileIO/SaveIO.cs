using System.IO;
using UnityEngine;

public static class SaveIO
{
	private static readonly string baseSavePath;

	static SaveIO()
	{
		baseSavePath = Application.persistentDataPath;
	}

	public static void SaveGameProgress(GameProgressSaveData progress)
	{
		var filePath = Path.Combine(baseSavePath, "progress.dat");
		Debug.Log($"filePath = {filePath}");

		FileReadWrite.WriteToBinaryFile(filePath, progress);
	}

	public static GameProgressSaveData LoadGameProgress()
	{
		var filePath = Path.Combine(baseSavePath, "progress.dat");
		Debug.Log($"filePath = {filePath}");		

		if (File.Exists(filePath))
		{
			return FileReadWrite.ReadFromBinaryFile<GameProgressSaveData>(filePath);
		}
		return null;
	}
}