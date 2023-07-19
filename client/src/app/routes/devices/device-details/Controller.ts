import { Controller } from "cx/ui";
import { GET, PUT } from "../../../api/util/methods";
import { debounce } from "cx/util";
import { HexXYColorMap } from "../../../api/util/colors";

export default class extends Controller {
    onInit(): void {
        this.loadData();
        this.store.set("$page.colors", HexXYColorMap);
        this.addTrigger("save-device", ["$page.device"], debounce(this.saveData, 300));
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

    openConfiguration() {}
}
