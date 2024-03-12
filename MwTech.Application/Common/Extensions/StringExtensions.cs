using MwTech.Application.Common.Models;


namespace MwTech.Application.Common.Extensions;
public static class StringExtensions
{
    public static string StripText(this string val)
    {
        string input = val.Trim().ToLower();
        string tmp = input.Replace("ą", "a");
        tmp = tmp.Replace("Ą", "A");
        tmp = tmp.Replace("ć", "c");
        tmp = tmp.Replace("Ć", "C");
        tmp = tmp.Replace("ę", "e");
        tmp = tmp.Replace("Ę", "E");
        tmp = tmp.Replace("ł", "l");
        tmp = tmp.Replace("Ł", "L");
        tmp = tmp.Replace("ń", "n");
        tmp = tmp.Replace("Ń", "N");
        tmp = tmp.Replace("ó", "o");
        tmp = tmp.Replace("Ó", "O");
        tmp = tmp.Replace("ś", "s");
        tmp = tmp.Replace("Ś", "S");
        tmp = tmp.Replace("ź", "z");
        tmp = tmp.Replace("Ź", "Z");
        tmp = tmp.Replace("ż", "z");
        tmp = tmp.Replace("Ż", "Z");
        string output = tmp; //.Replace(" ", "-");
        return output;
    }

    public static string AddBr(this string val)
    {
        if (val == null)
        {
            return string.Empty;
        }
        string tmp = val.Replace(System.Environment.NewLine, "<br />");
        return tmp;
    }

    public static string AutoTrim(this string code)
    {
        string newline = Environment.NewLine;
        var trimLen = code
            .Split(newline)
            .Skip(1)
            .Min(s => s.Length - s.TrimStart().Length);

        return string.Join(newline,
            code
            .Split(newline)
            .Select(line => line.Substring(Math.Min(line.Length, trimLen))));
    }
}
