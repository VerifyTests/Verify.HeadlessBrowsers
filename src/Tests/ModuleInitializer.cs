using System.Runtime.CompilerServices;
using VerifyTests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        #region Enable

        VerifySelenium.Enable();

        #endregion

        VerifyPhash.RegisterComparer("png", .99f);
    }
}