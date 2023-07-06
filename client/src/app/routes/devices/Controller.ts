import { Controller } from "cx/ui";
import { DeviceLogItem, DevicesPageModel } from "./page";
import { showErrorToast, showInfoToast } from "../../util/toasts";
import { append, createAccessorModelProxy } from "cx/data";
import { Status } from "../../types/status";
import { UpdateDevicePayload, getDevices, updateDevice } from "../../api/devices";
import * as signalR from "@microsoft/signalr";
import { Device } from "../../types/device";

let { $page } = createAccessorModelProxy<DevicesPageModel>();

export default class extends Controller<DevicesPageModel> {
    onInit() {
        this.onGetDevices();
        this.store.init($page.deviceLog, []);

        this.startDevicesLog();

        this.addTrigger("selected-device-changes", [$page.selectedDeviceId], (id) => {
            if (!id) return;
            const devices = this.store.get($page.devices);
            let device = JSON.parse(JSON.stringify(devices.find((d) => d.id === id)));

            delete device.id;
            delete device.capabilities;

            this.store.set($page.devicePayload, JSON.stringify(device));
        });
    }

    async onGetDevices() {
        try {
            this.store.set($page.status.request, Status.Loading);
            const devices = await getDevices();
            this.store.set($page.devices, devices);
            this.store.set($page.status.request, Status.Ok);
        } catch (err) {
            this.store.set($page.status.request, Status.Error);
            console.error(err);
            showErrorToast("Failed fetching devices");
        }
    }

    async startDevicesLog() {
        this.store.set($page.status.deviceHistory, Status.Loading);
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("http://127.0.0.1:5288/api/hubs/devices")
            .configureLogging(signalR.LogLevel.Information)
            .build();

        await connection.start();
        let i = 0;

        this.store.set($page.status.deviceHistory, Status.Ok);

        connection.stream("DevicesStream").subscribe({
            next: (item: Device) => {
                this.store.update($page.deviceLog, append, { ...item, order: i++ } as DeviceLogItem);
            },
            complete: () => {
                console.log("END STREAM");
            },
            error: (err) => {
                this.store.set($page.status.deviceHistory, Status.Error);
            },
        });
    }

    async onSendPayload() {
        try {
            this.store.set($page.status.deviceUpdate, Status.Loading);
            const payload = this.store.get($page.devicePayload);
            const data = JSON.parse(payload) as UpdateDevicePayload;
            const id = this.store.get($page.selectedDeviceId);

            await updateDevice(id, data);
            showInfoToast("Device state updated");
        } catch (err) {
            showErrorToast(err);
            console.error(err);
        } finally {
            this.store.set($page.status.deviceUpdate, Status.Ok);
        }
    }
}
