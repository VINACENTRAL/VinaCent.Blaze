namespace VinaCent.Blaze.Themes
{
    public interface ITheme
    {
        string GetLayout(string name, bool fallbackToDefault = true);
    }
}
