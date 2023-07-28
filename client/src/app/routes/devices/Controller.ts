import { Controller } from "cx/ui";
import { showErrorToast, showInfoToast } from "../../util/toasts";
import { append, createAccessorModelProxy } from "cx/data";
import { Status } from "../../types/status";
import { UpdateDevicePayload, getDevices, updateDevice } from "../../api/devices";
import * as signalR from "@microsoft/signalr";
import { Device } from "../../types/device";
import { DeviceLogItem, DevicesPageModel } from "./page";

let { $page } = createAccessorModelProxy<DevicesPageModel>();

export default class extends Controller<DevicesPageModel> {
    onInit() {
        this.onGetDevices();
    }

    async onGetDevices() {
        try {
            this.store.set($page.status.request, Status.Loading);
            const devices = await getDevices();
            this.store.set(
                $page.devices,
                devices.filter((device) => device.capabilities.length != 0)
            );
            this.store.set($page.status.request, Status.Ok);
        } catch (err) {
            this.store.set($page.status.request, Status.Error);
            console.error(err);
            showErrorToast("Failed fetching devices");
        }
    }
}
