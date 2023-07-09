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

    private const string RegexPattern = @"[vV]?\d+\.\d+\.{0,}\d{0,}\.{0,}\d{0,}(-release$|-beta\.\d+)*$";
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
                _ => VersionType.Release
            };
            Suffix = verSuffix.Split('.').Length == 2 ? int.Parse(verSuffix.Split('.')[1]) : 1;
        } catch (Exception e) {
            Console.WriteLine(e);
            throw new ArgumentException("Provided version string is invalid.", nameof(name), e);
        }
    }
    
    // operators
    public static bool operator ==(VersionName a, VersionName b) =>
        a.Major == b.Major &&
        a.Minor == b.Minor &&
        a.Patch == b.Patch &&
        a.Build == b.Build &&
        a.VersionType == b.VersionType &&
        a.Suffix == b.Suffix;

    public static bool operator !=(VersionName a, VersionName b) => !(a == b);

    public static bool operator >(VersionName a, VersionName b) {
        if (a == b) return false;
        if (a.Major != b.Major) return a.Major > b.Major;
        if (a.Minor != b.Minor) return a.Minor > b.Minor;
        if (a.Patch != b.Patch) return a.Patch > b.Patch;
        if (a.Build != b.Build) return a.Build > b.Build;
        if (a.VersionType != b.VersionType)
            return a.VersionType == VersionType.Release && b.VersionType == VersionType.Beta;
        return a.Suffix > b.Suffix;
    }

    public static bool operator <(VersionName a, VersionName b) => b > a;

    public static bool operator >=(VersionName a, VersionName b) => a == b || a > b;

    public static bool operator <=(VersionName a, VersionName b) => a == b || a < b;

    /// <summary>
    /// Gets the string value of the VersionName object.
    /// </summary>
    /// <param name="includePrefix">Include a "v" prefix in the return value.</param>
    /// <param name="includeZeroes">Include patch and build numbers even if they are zero.</param>
    /// <param name="includeType">Include the version type even if it is a release version.</param>
    /// <returns></returns>
    public string ToString(bool includePrefix, bool includeZeroes = false, bool includeType = true) {
        var val = "";
        if (includePrefix) val += "v";
        val += $"{Major}.{Minor}";
        if (includeZeroes || Patch != 0) val += $".{Patch}";
        if (includeZeroes || Build != 0) val += $".{Build}";
        if (includeType || VersionType != VersionType.Release) val += $"-{VersionType.ToString().ToLower()}";
        if (VersionType != VersionType.Release) val += $".{Suffix}";
        return val;
    }

    public override string ToString() => ToString(true, false, false);
}