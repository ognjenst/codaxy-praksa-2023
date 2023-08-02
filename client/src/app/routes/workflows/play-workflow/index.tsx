import { Button, TextArea, Window, ValidationGroup, TextField } from "cx/widgets";
import Controller from "./Controller";
import { CodeMirror } from "../../../components/CodeMirror";

export const openPlayWorkflowWindow = ({ props }) => {
    let w = Window.create(
        <cx>
            <Window
                title="Start Workflow"
                center
                className="p-4 h-[84vh] w-[60vw]"
                modal
                draggable
                closeOnEscape
                controller={Controller(props)}
            >
                <div style={{ padding: "20px" }}>
                    <ValidationGroup invalid-bind="$page.flagWorkflowInputData">
                        <TextArea
                            label="Workfow Input Data"
                            value-bind="$page.workflowInputData"
                            style={{ width: "100%" }}
                            rows={10}
                            minLength={2}
                            required
                            className="h-[60vh]"
                        />
                    </ValidationGroup>
                    <Button
                        onClick={(e, { controller, store }) => {
                            controller.invokeMethod("startWorkflow");
                        }}
                        text="Start Workflow"
                        style={{ width: "100%" }}
                        disabled-bind="$page.flagWorkflowInputData"
                    />
                </div>
            </Window>
        </cx>
    );

    w.open();
};
