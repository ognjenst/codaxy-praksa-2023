namespace SOC.IoT.ApiGateway.Entities.Enums;

public static class PermissionData
{
    public static List<string> Scopes = new List<string>
    {
        "Create", "Read", "Update", "Delete"
    };

    public static List<string> Resources = new List<string>
    {
        "Workflow", "Trigger", "Automation", "Device", "DeviceHistory"
    };

}
