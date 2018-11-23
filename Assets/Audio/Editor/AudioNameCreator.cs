using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

/// <summary>
/// オーディオのファイル名を定数で管理するクラスを作成するスクリプト
/// </summary>
public static class AudioNameCreator
{

    private const string COMMAND_NAME = "Tools/Create/Audio Name";        // コマンド名
    private const string EXPORT_PATH = "Assets/Plugins/Constants/AUDIO.cs"; //作成したスクリプトを保存するパス

    // ファイル名(拡張子あり、なし)
    private static readonly string FILENAME = Path.GetFileName(EXPORT_PATH);
    private static readonly string FILENAME_WITHOUT_EXTENSION = Path.GetFileNameWithoutExtension(EXPORT_PATH);

    /// <summary>
    /// オーディオのファイル名を定数で管理するクラスを作成します
    /// </summary>
    [MenuItem(COMMAND_NAME)]
    public static void Create()
    {
        if (!CanCreate())
        {
            return;
        }

        CreateScript();

        EditorUtility.DisplayDialog(FILENAME, "作成が完了しました", "OK");
    }

    /// <summary>
    /// スクリプトを作成します
    /// </summary>
    public static void CreateScript()
    {
        StringBuilder builder = new StringBuilder();

        builder.AppendLine("/// <summary>");
        builder.AppendLine("/// オーディオ名を定数で管理するクラス");
        builder.AppendLine("/// 使用例　");
        builder.AppendLine("/// string seName = AUDIO.SE_VANISHING;");
        builder.AppendLine("/// ※AUDIO class　に格納してある、データの名前が返ってくる。");
        builder.AppendLine("/// </summary>");
        builder.AppendFormat("public static class {0}", FILENAME_WITHOUT_EXTENSION).AppendLine();
        builder.AppendLine("{");

        //指定したパスのリソースを全て取得
        object[] bgmList = Resources.LoadAll("Audio/BGM");
        object[] seList = Resources.LoadAll("Audio/SE");

        foreach (AudioClip bgm in bgmList)
        {
            builder.Append("\t").AppendFormat(@"  public const string {0} = ""{1}"";", bgm.name.ToUpper(), bgm.name).AppendLine();
        }

        builder.AppendLine("\t");

        foreach (AudioClip se in seList)
        {
            builder.Append("\t").AppendFormat(@"  public const string {0} = ""{1}"";", se.name.ToUpper(), se.name).AppendLine();
        }

        builder.AppendLine("}");

        string directoryName = Path.GetDirectoryName(EXPORT_PATH);
        if (!Directory.Exists(directoryName))
        {
            Directory.CreateDirectory(directoryName);
        }

        File.WriteAllText(EXPORT_PATH, builder.ToString(), Encoding.UTF8);
        AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
    }

    /// <summary>
    /// オーディオのファイル名を定数で管理するクラスを作成できるかどうかを取得します
    /// </summary>
    [MenuItem(COMMAND_NAME, true)]
    private static bool CanCreate()
    {
        return !EditorApplication.isPlaying && !Application.isPlaying && !EditorApplication.isCompiling;
    }

}

public class Intelior_List : AssetPostprocessor
{
    void OnPostprocessAudio()
    {
        AudioNameCreator.Create();
    }
}