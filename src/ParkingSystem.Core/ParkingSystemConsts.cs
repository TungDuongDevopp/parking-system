using ParkingSystem.Debugging;

namespace ParkingSystem;

public class ParkingSystemConsts
{
    public const string LocalizationSourceName = "ParkingSystem";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = false;


    /// <summary>
    /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
    /// </summary>
    public static readonly string DefaultPassPhrase =
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "136c77d18402465097091ba148fba30e";
}
