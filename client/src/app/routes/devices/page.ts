import { Device } from "../../types/device";
import { Status } from "../../types/status";

export interface DevicesPageModel {
    $page: {
        devices: Device[];
        deviceLog: DeviceLogItem[];
        status?: {
            request?: Status;
            deviceHistory?: Status;
            deviceUpdate?: Status;
        };
        selectedDeviceId: string;
        devicePayload: string;
    };

    $log: DeviceLogItem;
    $device: Device;
    $capability: string;
}

export interface DeviceLogItem extends Device {
    order: number;
}
