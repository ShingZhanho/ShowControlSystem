using System.Text.RegularExpressions;

namespace ShowControlSystem.Common; 

/// <summary>
/// Represents the app's version. E.g. v12.3.4.098-beta.3
/// </summary>
public struct VersionName {
    public int Major { get; set; }
    public int Minor { get; set; }
    public int Patch { get; set; } = 0;
    public int Build { get; set; } = 0;
    public VersionType VersionType { get; set; } = VersionType.Release;
    public int Suffix { get; set; } = 1;

    private const string RegexPattern = @"[vV]?\d+\.\d+\.{0,}\d{0,}\.{0,}\d{0,}(-release$|-beta\.\d+|-hotfix(\.+\d+)*)*$";
    /// <summary>
    /// Create a VersionName object from a version string.
    /// </summary>
    /// <param name="name">The version string to be created from. Prefix "v" is not mandatory.</param>
    public VersionName(string name) {
        try {
            // check using regex
            var r = new Regex(RegexPattern, RegexOptions.IgnoreCase);
            if (r.Match(name).Value != name) throw new Exception();
            
            // remove prefix "v"
            if (name.StartsWith('v') | name.StartsWith('V'))
                name = name[1..];

            // split by hyphen
            var mainVer = name.Split('-')[0];
            var verSuffix = name.Split('-')[1];
            
            // process digits
            Major = int.Parse(mainVer.Split('.')[0]);
            Minor = int.Parse(mainVer.Split('.')[1]);
            Patch = mainVer.Split('.').Length >= 3 ? int.Parse(mainVer.Split('.')[2]) : 0;
            Build = mainVer.Split('.').Length == 4 ? int.Parse(mainVer.Split('.')[3]) : 0;
            
            // process suffix
            VersionType = verSuffix.Split('.')[0].ToLower() switch {
                "release" => VersionType.Release,
                "beta" => VersionType.Beta,
                "hotfix" => VersionType.Hotfix,
                _ => VersionType.Release
            };
            Suffix = verSuffix.Split('.').Length == 2 ? int.Parse(verSuffix.Split('.')[1]) : 1;
        } catch (Exception e) {
            Console.WriteLine(e);
            throw new ArgumentException("Provided version string is invalid.", nameof(name), e);
        }
    }
}