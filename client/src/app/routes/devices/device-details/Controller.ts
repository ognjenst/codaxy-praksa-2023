import { Controller } from "cx/ui";
import { GET } from "../../../api/util/methods";

export default class extends Controller {
    onInit(): void {
        this.loadData();
        this.store.set("$page.colors", HexXYColorMap);
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

    openConfiguration() {}
}
