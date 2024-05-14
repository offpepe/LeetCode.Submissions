namespace LeetCode.Attributes;

public class FailedAttribute(string reason) : Attribute
{
    public virtual string Reason { get; } = reason;
}