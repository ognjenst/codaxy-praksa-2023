import { LabelsLeftLayout, PureContainer, Repeater, Text, bind, computable } from "cx/ui";
import { Button, Checkbox, DateField, HtmlElement, Label, Overlay, TextArea, TextField, Window } from "cx/widgets";
import Controller from "./Controller";

export const openDryRunWindow = ({ task }) => {
    let w = Window.create(
        <cx>
            <Window title="Dry run" center style={{ width: "400px" }} modal draggable closeOnEscape controller={Controller(task)}>
                <div style={{ padding: "50px" }}>
                    <TextField readOnly label="Expression" style={{ width: "100%" }} value={task.expression} />
                    <Repeater records={bind("$inputs")}>
                        <TextField label-bind="$record.tab" value-bind="$record.value" style={{ width: "100%" }} tooltip="A Tooltip" />
                    </Repeater>
                    <TextField
                        readOnly
                        label="Result"
                        value={computable("$result", (result) => (result ? "true" : "false"))}
                        style={{ width: "100%" }}
                    />
                </div>
                <div></div>
                <div putInto="footer" style={{ float: "right" }}>
                    <Button
                        mod="primary"
                        text="Run"
                        onClick={(e, { controller, store }) => {
                            controller.invokeMethod("getEval");
                        }}
                    />
                </div>
            </Window>
        </cx>
    );

    w.open();
};
