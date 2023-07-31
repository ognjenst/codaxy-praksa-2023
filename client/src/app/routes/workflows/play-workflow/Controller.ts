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
            var obj = this.getJson();

            if (obj === null) {
                MsgBox.alert(INPUT_DATA_FAIL);

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

            console.log(request);

            try {
                let resp = await POST(BACKEND_REQUEST_PLAY_WORKFLOW, request);

                this.store.set("$page.workflowInputData", defaultValue);

                MsgBox.alert(START_WORKFLOW_SUCCESS);
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

const START_WORKFLOW_SUCCESS = "Successfully started a worklow :)";
const INPUT_DATA_FAIL = "You need to input a json object !!!";
const BACKEND_REQUEST_PLAY_WORKFLOW = "/workflows/playworkflow";
