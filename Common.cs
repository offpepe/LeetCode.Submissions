using LeetCode.Enums;

namespace LeetCode;

public static class Common
{
    public static string Centralize(this string content)
    {
        for (short i = 0; i < 20; i++)
        {
            content = ' ' + content + ' ';
        }

        return content;
    }
    
    public static string AddPaddings(this object? obj, short size)
    {
        var param = obj!.ToString() ?? string.Empty;
        if (param.Length >= size) return param;
        while (param.Length < size)
        {
            param += ' ';
        }
        return param;
    }
    
    public static string AddPaddings(this object? obj, short size, PaddingDirection direction)
    {
        var param = obj!.ToString() ?? string.Empty;
        if (param.Length >= size) return param;
        while (param.Length < size)
        {
            switch (direction)
            {
                case PaddingDirection.Right:
                    param = ' ' + param;
                    break;
                case PaddingDirection.Both:
                    param = ' ' + param + ' ';
                    break;
                default:
                    param += ' ';
                    break;
            }
        }
        return param;
    }

}