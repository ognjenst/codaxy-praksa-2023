import { Controller } from "cx/ui";
import { GET } from "../../../api/util/methods";
import { POST } from "../../../api/util/methods";
import { MsgBox } from "cx/widgets";

export default (props) =>
    class extends Controller {
        onInit(): void {
            this.store.set("$page.workflowInputData", defaultValue);
        }

        async startWorkflow() {
            var obj = JSON.parse(this.store.get("$page.workflowInputData"));

            var map = new Map();

            Object.keys(obj).forEach(function (key) {
                map.set(key, obj[key]);
            });

            var request = {
                name: props.name,
                version: props.version,
                input: obj,
            };

            console.log(request);

            try {
                let resp = await POST("/workflows/playworkflow", request);

                this.store.set("$page.workflowInputData", defaultValue);

                MsgBox.alert("Successfully started a worklow :)");
            } catch (err) {
                console.error(err);
            }
        }
    };

const defaultValue = "{}";
