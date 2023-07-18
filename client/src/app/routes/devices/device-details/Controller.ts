import { Controller } from "cx/ui";
import { GET } from "../../../api/util/methods";

export default class extends Controller {
    onInit(): void {
        this.loadData();
        this.store.set("$page.colors", colorMap);
    }

    async loadData() {
        let id = this.store.get("$route.id");
        try {
            let resp = await GET(`/devices/${id}`);
            this.store.set("$page.device", resp);
        } catch (err) {
            console.error(err);
        }
    }
}

const colorMap = {
    "#FF0000": { x: 0.6942, y: 0.2963 },
    "#FFC600": { x: 0.5528, y: 0.4079 },
    "#FFFF00": { x: 0.4339, y: 0.5008 },
    "#00FF00": { x: 0.1704, y: 0.709 },
    "#0000FF": { x: 0.1355, y: 0.0399 },
    "#86007D": { x: 0.1355, y: 0.0399 },
};
