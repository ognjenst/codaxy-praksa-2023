import { Controller } from "cx/ui";
import { GET } from "../../../api/util/methods";
import { POST } from "../../../api/util/methods";
import { MsgBox } from "cx/widgets";
import WorkflowVariables from "../WorkflowVariables";

export default (props) =>
    class extends Controller {
        onInit(): void {
            this.store.set("$page.workflowInputData", defaultValue);
        }

        async startWorkflow() {
            var obj = this.getJson();

            if (obj === null) {
                MsgBox.alert("You need to input a json object !!!");

                return;
            }

            var map = new Map();

            Object.keys(obj).forEach(function (key) {
                map.set(key, obj[key]);
            });

            var request = {
                name: props.name,
                version: props.version,
                input: obj,
            };

            try {
                let resp = await POST(WorkflowVariables.BACKEND_REQUEST_PLAY_WORKFLOW, request);

                this.store.set("$page.workflowInputData", defaultValue);

                MsgBox.alert("Successfully started a worklow :)");
            } catch (err) {
                console.error(err);
            }
        }

        getJson() {
            try {
                var obj = JSON.parse(this.store.get("$page.workflowInputData"));

                if (typeof obj == "object") {
                    return obj;
                }

                return obj;
            } catch (err) {}

            return null;
        }
    };

const defaultValue = "{}";
