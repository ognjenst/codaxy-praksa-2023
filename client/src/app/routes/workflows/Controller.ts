import { Controller } from "cx/ui";
import { GET } from "../../api/util/methods";
import { openInsertUpdateWindow } from "./update-insert-workflow";

export default class extends Controller {
    onInit(): void {
        this.store.set("$page.flagPauseStopStatus", false);
    }

    async openWindow() {
        let newObj = await openInsertUpdateWindow({
            props: {
                action: "Insert",
            },
        });

        if (!newObj) return;

        this.store.set("$page.undoneWorkflows", [...this.store.get("$page.undoneWorkflows"), newObj]);
    }
}
