namespace IoT.Base.Infrastructure;

internal interface IZigbeeService
{
    Task DiscoverDevicesAsync();
}
