import { Button, TextArea, Window, ValidationGroup } from "cx/widgets";
import Controller from "./Controller";

export const openPlayWorkflowWindow = () => {
    let w = Window.create(
        <cx>
            <Window title="Start Workflow" center style={{ width: "600px" }} modal draggable closeOnEscape controller={Controller}>
                <div style={{ padding: "20px" }}>
                    <ValidationGroup invalid-bind="$page.flagWorkflowInputData">
                        <TextArea
                            label="Workfow Input Data"
                            value-bind="$page.workflowInputData"
                            style={{ width: "100%" }}
                            rows={10}
                            minLength={1}
                            required
                        />
                    </ValidationGroup>
                    <Button
                        onClick="startWorkflow"
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
