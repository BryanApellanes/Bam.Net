using System;
public class FuncProvider
{
    public Func<bool> GetFunc()
    {
        return (Func<bool>)(() => true);
    }
}