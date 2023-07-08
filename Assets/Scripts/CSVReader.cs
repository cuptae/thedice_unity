using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    //public TextAsset csvFile;
    //public char fieldSeparator = ',';

    //public List<List<string>> csvData;

    //private void Start()
    //{
    //    ReadCSVFile();
    //}

    //private void ReadCSVFile()
    //{
    //    csvData = new List<List<string>>();

    //    string[] lines = csvFile.text.Split('\n');

    //    for (int i = 0; i < lines.Length; i++)
    //    {
    //        string[] fields = lines[i].Split(fieldSeparator);
    //        List<string> rowData = new List<string>();

    //        for (int j = 0; j < fields.Length; j++)
    //        {
    //            rowData.Add(fields[j]);
    //        }
    //        csvData.Add(rowData);
    //    }
    //}
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
}
