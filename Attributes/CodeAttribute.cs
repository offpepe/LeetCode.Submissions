namespace LeetCode.Attributes;

public class CodeAttribute : Attribute
{
    public CodeAttribute(int code) => Code = code;
    
    public virtual int Code { get; }    
    
}