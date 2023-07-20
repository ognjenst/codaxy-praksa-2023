import { LabelsLeftLayout, PureContainer, Repeater, Text, bind } from "cx/ui";
import { Button, Checkbox, DateField, HtmlElement, Label, Overlay, TextArea, TextField, Window } from "cx/widgets";
import Controller from "./Controller";

export const openDryRunWindow = ({ task }) => {
    let w = Window.create(
        <cx>
            <Window title="Dry run" center style={{ width: "400px" }} modal draggable closeOnEscape controller={Controller}>
                <div style={{ padding: "50px" }}>
                    <TextField readOnly label="Expression" style={{ width: "100%" }} value={task.expression} />
                    <Repeater records={task.inputs}>
                        <TextField label-bind="$record.tab" style={{ width: "100%" }} tooltip="A Tooltip" />
                    </Repeater>
                    <TextField readOnly label="Result" style={{ width: "100%" }} value="True" />
                </div>
                <div></div>
                <div putInto="footer" style={{ float: "right" }}>
                    <Button mod="primary" text="Run" />
                </div>
            </Window>
        </cx>
    );

    w.open();
};
