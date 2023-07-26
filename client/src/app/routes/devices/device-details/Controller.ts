import { Controller } from "cx/ui";
import { GET, PUT } from "../../../api/util/methods";
import { debounce } from "cx/util";
import { HexXYColorMap } from "../../../api/util/colors";
import { openHistoryWindow } from "./showHistoryWindow";
import * as signalR from "@microsoft/signalr";

const deviceUrl = "http://127.0.0.1:5288/api/hubs/devices";

export default class extends Controller {
    async onInit() {
        await this.loadData();
        this.store.set("$page.colors", HexXYColorMap);

        await this.loadDeviceHistory();

        const connection = new signalR.HubConnectionBuilder().withUrl(deviceUrl).configureLogging(signalR.LogLevel.Information).build();

        await connection.start();
        connection.stream("DeviceStream", this.store.get("$page.device.id")).subscribe({
            next: (item) => {
                const date = new Date();
                this.store.set("$page.deviceHistory", [
                    { time: date.toLocaleString(), configuration: JSON.stringify(item, null, 4) },
                    ...this.store.get("$page.deviceHistory"),
                ]);

                this.store.set(
                    "$page.powerChart",
                    [
                        ...this.store.get("$page.powerChart"),
                        { x: date.toLocaleTimeString(), y: this.store.get("$page.device.energy.power") },
                    ].slice(-30)
                );

                this.loadData();
            },
            complete: () => {
                console.log("END");
            },
            error: (err) => {
                console.error(err);
            },
        });
    }

    async loadDeviceHistory() {
        let id = this.store.get("$route.id");
        try {
            let deviceHistory = await GET(`/devicehistory/${id}`);
            this.store.set("$page.deviceHistory", deviceHistory);
            let powerChart = deviceHistory
                .map((item) => {
                    return { x: item.time, y: JSON.parse(item.configuration).energy.power };
                })
                .slice(-30);
            this.store.set("$page.powerChart", powerChart);
        } catch (err) {
            console.error(err);
        }
    }

    async loadData() {
        this.removeTrigger("save-device");

        let id = this.store.get("$route.id");
        try {
            let device = await GET(`/devices/${id}`);
            this.store.set("$page.device", device);
        } catch (err) {
            console.error(err);
        } finally {
            this.addTrigger("save-device", ["$page.device"], debounce(this.saveData, 300));
        }
    }

    async saveData(device) {
        try {
            await PUT(`/devices/${device.id}`, device);
        } catch (err) {
            console.error(err);
        }
    }

    async changeColors(e, { store }) {
        let color = store.get("$record");
        let colorXy = HexXYColorMap[color];
        this.store.set("$page.device.colorXy", colorXy);
    }

    showHistoryWindow(e, { store }) {
        let info = store.get("$record");
        openHistoryWindow(info);
    }
}
