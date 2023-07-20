import { Controller } from "cx/ui";
import { GET, PUT } from "../../../api/util/methods";
import { debounce } from "cx/util";
import { HexXYColorMap } from "../../../api/util/colors";
import { openHistoryWindow } from "./showHistoryWindow";
import * as signalR from "@microsoft/signalr";

export default class extends Controller {
    async onInit() {
        await this.loadData();
        this.store.set("$page.colors", HexXYColorMap);
        this.addTrigger("save-device", ["$page.device"], debounce(this.saveData, 300));
        this.store.set("$page.deviceHistory", []);

        const connection = new signalR.HubConnectionBuilder()
            .withUrl("http://127.0.0.1:5288/api/hubs/devices")
            .configureLogging(signalR.LogLevel.Information)
            .build();

        await connection.start();
        connection.stream("DeviceStream", this.store.get("$page.device.id")).subscribe({
            next: (item) => {
                const date = new Date();
                this.store.set("$page.deviceHistory", [
                    { timestamp: date.toLocaleString(), configuration: JSON.stringify(item, null, 4) },
                    ...this.store.get("$page.deviceHistory"),
                ]);
                this.store.set("$page.device", { ...this.store.get("$page.device"), ...item });
            },
            complete: () => {
                console.log("END");
            },
            error: (err) => {
                console.error(err);
            },
        });
    }

    async loadData() {
        let id = this.store.get("$route.id");
        try {
            let device = await GET(`/devices/${id}`);
            this.store.set("$page.device", device);
        } catch (err) {
            console.error(err);
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
