namespace LeetCode.Attributes;

public class CompletedAttribute(bool isCompleted) : Attribute
{
    public bool Completed => isCompleted;
}