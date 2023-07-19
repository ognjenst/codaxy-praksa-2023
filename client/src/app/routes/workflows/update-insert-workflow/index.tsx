import { LabelsLeftLayout, PureContainer, Repeater, Text, bind } from "cx/ui";
import { Button, Checkbox, DateField, HtmlElement, Label, Overlay, TextArea, NumberField, TextField, Window, List } from "cx/widgets";
import Controller from "./Controller";

export const openInsertUpdateWindow = ({ props }) => {
    let w = Window.create(
        <cx>
            <Window title={props.action} center className="p-4" modal draggable closeOnEscape controller={Controller}>
                <div className="grid grid-rows-3" layout={LabelsLeftLayout} styles={{ height: "600px" }}>
                    <div layout={LabelsLeftLayout}>
                        <TextField label="Name" value={props.name} />
                        <TextField label="Description" value={props.description} />
                        <NumberField label="version" value={props.version} />
                    </div>
                    <div className="grid grid-cols-2">
                        <div>
                            <List records-bind="$insert.arrTasks">
                                <span className="mr-2" text-bind="$record.name" />
                                <Button
                                    icon="plus"
                                    onClick={(e, { store }) => {
                                        store.set("$insert.workflowTasks", [...store.get("$insert.workflowTasks"), store.get("$record")]);
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
                        <Button className="w-full">Submit</Button>
                    </div>
                </div>
            </Window>
        </cx>
    );

    w.open();
};
