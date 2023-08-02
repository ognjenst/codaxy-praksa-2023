import { Controller } from "cx/ui";
import { GET } from "../../api/util/methods";
import { openInsertUpdateWindow } from "./update-insert-workflow";

export default class extends Controller {
    onInit(): void {
        this.store.set("$page.flagPauseStopStatus", false);
    }

    async openWindow() {
        var arrNames = [];

        var arrFor = (arrList) => {
            for (let i = 0; i < arrList.length; i++) {
                arrNames.push(arrList[i].name);
            }
        };

        arrFor(this.store.get("$page.workflows"));
        arrFor(this.store.get("$page.undoneWorkflows"));

        let newObj = await openInsertUpdateWindow({
            props: {
                action: "Insert",
                arr: arrNames,
            },
        });

        if (!newObj) return;

        this.store.set("$page.undoneWorkflows", [...this.store.get("$page.undoneWorkflows"), newObj]);
    }
}
