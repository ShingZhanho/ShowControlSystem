namespace ShowControlSystem.Common.File; 

/// <summary>
/// An object containing all information of a rundown.
/// Should be created through <see cref="RundownCreator"/>.
/// </summary>
public class Rundown {
    #region Properties
    
    // metadata section
    /// <summary>
    /// Specifies the schema version used when parsing.
    /// Note that the schema version will be updated to the latest version upon saving.
    /// </summary>
    public int Schema { get; /* set; (usage of setter is in doubt) */  }
    /// <summary>
    /// Specifies the minimum compatible version of client software for dealing with
    /// this rundown.
    /// </summary>
    public VersionName MinimumCompatibleVersion { get; }
    public string Name { get; set; }
    public DateOnly Date { get; set; }

    #endregion
}