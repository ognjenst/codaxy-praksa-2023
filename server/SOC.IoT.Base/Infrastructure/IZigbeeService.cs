namespace SOC.IoT.Base.Infrastructure;

internal interface IZigbeeService
{
    Task DiscoverDevicesAsync();
}
