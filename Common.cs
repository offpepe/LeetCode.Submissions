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
}