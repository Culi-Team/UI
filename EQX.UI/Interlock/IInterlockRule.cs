namespace EQX.UI.Interlock
{
    public interface IInterlockRule
    {
        string Key { get; }
        bool IsSatisfied(InterlockContext ctx);
    }
}
