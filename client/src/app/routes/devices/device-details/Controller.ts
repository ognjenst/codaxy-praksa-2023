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

                if (this.store.get("$page.device.energy") != null) {
                    this.store.set(
                        "$page.powerChart",
                        [{ x: date, y: this.store.get("$page.device.energy.power") }, ...this.store.get("$page.powerChart")].slice(0, 30)
                    );
                }
                if (this.store.get("$page.device.temperature") != null) {
                    this.store.set(
                        "$page.temperatureChart",
                        [
                            { x: date, y: this.store.get("$page.device.temperature.value") },
                            ...this.store.get("$page.temperatureChart"),
                        ].slice(0, 20)
                    );
                }
                if (this.store.get("$page.device.humidity") != null) {
                    this.store.set(
                        "$page.humidityChart",
                        [{ x: date, y: this.store.get("$page.device.humidity.value") }, ...this.store.get("$page.humidityChart")].slice(
                            0,
                            20
                        )
                    );
                }
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
            let deviceHistory = (await GET(`/devicehistory/${id}`)).sort((x, y) => {
                return y.time.localeCompare(x.time);
            });
            this.store.set(
                "$page.deviceHistory",
                deviceHistory.map((item) => {
                    return { ...item, time: new Date(Date.parse(item.time)).toLocaleString() };
                })
            );
            if (this.store.get("$page.device.energy") != null) {
                let powerChart = deviceHistory
                    .map((item) => {
                        console.log(JSON.parse(item.configuration));
                        return { x: new Date(Date.parse(item.time)), y: JSON.parse(item.configuration).energy.power };
                    })
                    .slice(0, 30);
                this.store.set("$page.powerChart", powerChart);
            }
            if (this.store.get("$page.device.temperature") != null) {
                let temperatureChart = deviceHistory
                    .map((item) => {
                        return { x: new Date(Date.parse(item.time)), y: JSON.parse(item.configuration).temperature.value };
                    })
                    .slice(0, 20);
                this.store.set("$page.temperatureChart", temperatureChart);
            }
            if (this.store.get("$page.device.humidity") != null) {
                let humidityChart = deviceHistory
                    .map((item) => {
                        return { x: new Date(Date.parse(item.time)), y: JSON.parse(item.configuration).humidity.value };
                    })
                    .slice(0, 20);
                this.store.set("$page.humidityChart", humidityChart);
            }
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
