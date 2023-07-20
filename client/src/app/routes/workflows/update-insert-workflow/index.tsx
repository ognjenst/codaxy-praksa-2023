import { DataProxy, LabelsLeftLayout, PureContainer, Repeater, Text, bind } from "cx/ui";
import {
    Button,
    Checkbox,
    DateField,
    HtmlElement,
    Label,
    Overlay,
    TextArea,
    NumberField,
    TextField,
    Window,
    List,
    ValidationGroup,
} from "cx/widgets";
import Controller from "./Controller";

export const openInsertUpdateWindow = ({ props }) => {
    return new Promise((reslove) => {
        let w = Window.create(
            <cx>
                <Window
                    title={props.action}
                    center
                    className="p-4"
                    modal
                    draggable
                    closeOnEscape
                    controller={Controller}
                    onDestroy={() => reslove(false)}
                >
                    <div className="grid grid-rows-3" layout={LabelsLeftLayout} styles={{ height: "600px" }}>
                        <div className="grid grid-cols-2 gap-4">
                            <div layout={LabelsLeftLayout}>
                                <TextField label="Name" value={props.name} value-bind="$page.insertUpdateName" />
                                <TextField label="Description" value={props.description} value-bind="$page.insertUpdateDescription" />
                                <NumberField label="Version" value={props.version} value-bind="$page.insertUpdateVersion" />
                            </div>

                            <div>
                                <ValidationGroup invalid-bind="$insert.flagValidParamName">
                                    <TextField
                                        required
                                        placeholder="Insert param name"
                                        value-bind="$insert.singleParamName"
                                        minLength={2}
                                    />

                                    <Button
                                        disabled-bind="$insert.flagValidParamName"
                                        text="Add param"
                                        className="ml-2"
                                        onClick={(e, { store }) => {
                                            console.log(store.get("$insert.workflowParamNames"));
                                            store.set("$insert.workflowParamNames", [
                                                ...store.get("$insert.workflowParamNames"),
                                                store.get("$insert.singleParamName"),
                                            ]);
                                            store.set("$insert.singleParamName", "");
                                        }}
                                    />
                                </ValidationGroup>

                                <List records-bind="$insert.workflowParamNames" className="w-full" style={{ textAlign: "center" }}>
                                    <div text-bind="$record"></div>
                                </List>
                            </div>
                        </div>
                        <div className="grid grid-cols-2">
                            <div>
                                <List records-bind="$insert.arrTasks" className="w-full" style={{ textAlign: "center" }}>
                                    <span className="mr-2" text-bind="$record.name" />
                                    <Button
                                        icon="plus"
                                        onClick={(e, { store }) => {
                                            store.set("$insert.workflowTasks", [
                                                ...store.get("$insert.workflowTasks"),
                                                store.get("$record"),
                                            ]);
                                        }}
                                    ></Button>
                                </List>
                            </div>
                            <div>
                                <List records-bind="$insert.workflowTasks">
                                    <span className="mr-2" text-bind="$record.name" />
                                    <Button
                                        className="p-0 pl-2 pr-2"
                                        onClick={(e, { store }) => {
                                            var ind = store.get("$index");

                                            store.set(
                                                "$insert.workflowTasks",
                                                store.get("$insert.workflowTasks").filter((value, index, arr) => {
                                                    if (index === ind) {
                                                        return false;
                                                    }

                                                    return true;
                                                })
                                            );
                                        }}
                                    >
                                        <svg
                                            xmlns="http://www.w3.org/2000/svg"
                                            fill="none"
                                            viewBox="0 0 24 24"
                                            strokeWidth={1.5}
                                            stroke="currentColor"
                                            className="w-6 h-6"
                                        >
                                            <path strokeLinecap="round" strokeLinejoin="round" d="M18 12H6" />
                                        </svg>
                                    </Button>
                                </List>
                            </div>
                        </div>
                        <div>
                            <Button
                                className="w-full"
                                onClick={(e, { store }) => {
                                    var arrObject = {
                                        name: store.get("$page.insertUpdateName"),
                                        description: store.get("$page.insertUpdateDescription"),
                                        version: store.get("$page.insertUpdateVersion"),
                                        tasks: store.get("$insert.workflowTasks"),
                                    };

                                    reslove(arrObject);
                                }}
                                text="Submit"
                            />
                        </div>
                    </div>
                </Window>
            </cx>
        );

        w.open();
    });
};
