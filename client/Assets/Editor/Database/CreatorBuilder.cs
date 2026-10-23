using System.Collections.Generic;
using System.Text;

public class CreatorBuilder
{
    private readonly List<string> strList = new(256);

    private int TabCount = 0;

    public CreatorBuilder Enter() {
        TabCount++;
        return this;
    }

    public CreatorBuilder Quit(bool _newLine = true) {
        TabCount--;
        if (_newLine) {
            strList.Add("");
        }
        return this;
    }

    public CreatorBuilder Append(string _str) {
        strList.Add($"{GetTab()}{_str}");
        return this;
    }

    public CreatorBuilder EnterLine() {
        strList.Add("");
        return this;
    }

    public CreatorBuilder AddRange(IEnumerable<string> _it) {
        foreach (var t in _it) {
            Append(t);
        }
        return this;
    }

    private string GetTab() {
        switch (TabCount) {
            case 1:
                return DatabaseParserCreator.Tab;
            case 2:
                return DatabaseParserCreator.Tab2;
            case 3:
                return DatabaseParserCreator.Tab3;
            case 4:
                return DatabaseParserCreator.Tab4;
            case 5:
                return DatabaseParserCreator.Tab4 + DatabaseParserCreator.Tab;
            case 6:
                return DatabaseParserCreator.Tab4 + DatabaseParserCreator.Tab2;
            default:
                return "";
        }
    }

    public string ToString(string _split) {
        var sb = new StringBuilder();
        foreach (var s in strList) {
            sb.Append($"{s}{_split}");
        }
        return sb.ToString();
    }
}