export interface Device {
    id?: string;
    capabilities?: string[];
    state?: DeviceState;
    light?: DeviceLight;
    colorXy?: DeviceColorXy;
    temperature?: DeviceTemperature;
    humidity?: DeviceHumidity;
    energy?: DeviceEnergy;
    contact?: DeviceContact;
}

export interface DeviceState {
    state: boolean;
}

export interface DeviceLight {
    brightness: number;
}

export interface DeviceColorXy {
    x: number;
    y: number;
}

export interface DeviceTemperature {
    value: number;
    unit: string;
}

export interface DeviceHumidity {
    value: number;
    unit: string;
}

export interface DeviceEnergy {
    total?: number;
    current?: number;
    voltage?: number;
    power?: number;
}

export interface DeviceContact {
    value?: boolean;
}
