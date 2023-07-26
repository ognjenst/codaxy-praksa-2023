import { Controller } from "cx/ui";
import { GET } from "../../../api/util/methods";
import { POST } from "../../../api/util/methods";

export default (props) =>
    class extends Controller {
        onInit(): void {}

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

                console.log("POST response: " + resp);
            } catch (err) {
                console.log("Proslo ...");
                console.error(err);
            }
        }
    };
