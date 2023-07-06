import { Device, DeviceEnergy, DeviceHumidity, DeviceTemperature } from "../types/device";
import { GET, PUT } from "./util/methods";

export async function getDevices(): Promise<Device[]> {
    return await GET(`/devices`);
}

export async function updateDevice(id: string, payload: UpdateDevicePayload): Promise<void> {
    return await PUT(`/devices/${id}`, payload);
}

interface RemoveUpdateDeviceFields {
    id: string;
    capabilities: string[];
    temperature: DeviceTemperature;
    humidity: DeviceHumidity;
    energy: DeviceEnergy;
}
export interface UpdateDevicePayload extends Omit<Device, keyof RemoveUpdateDeviceFields> {}
