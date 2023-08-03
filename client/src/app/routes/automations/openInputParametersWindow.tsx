import { LabelsLeftLayout } from "cx/ui";
import { TextField, Window } from "cx/widgets";
import { CodeMirror } from "../../components/CodeMirror";

export const openInputParametersWindow = (info) => {
    let w = Window.create(
        <cx>
            <Window title="Configuration" center style="width: 50vw; height: 80vh;" modal closeOnEscape>
                <CodeMirror code={info.inputParameters} mode="text/javascript" lineSeparator={"\n"} readOnly style="height:100%" />
            </Window>
        </cx>
    );
    w.open();
};
