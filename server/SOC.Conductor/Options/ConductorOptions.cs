using System.ComponentModel.DataAnnotations;

namespace SOC.Conductor.Options;

public class ConductorOptions
{
    public static readonly string SectionName = "ConductorOptions";

    [Required]
    public string ConductorUrl { get; set; } = string.Empty;
}
