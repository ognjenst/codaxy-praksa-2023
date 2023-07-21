import { LabelsLeftLayout } from "cx/ui";
import { Button, List, NumberField, TextField, ValidationGroup, Window } from "cx/widgets";
import getController from "./Controller";

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
                    controller={getController(reslove, props)}
                    onDestroy={() => reslove(false)}
                >
                    <div className="grid grid-rows-3" layout={LabelsLeftLayout} styles={{ height: "600px" }}>
                        <div className="grid grid-cols-2 gap-4">
                            <div layout={LabelsLeftLayout}>
                                <TextField label="Name" value-bind="$page.insertUpdateName" />
                                <TextField label="Description" value-bind="$page.insertUpdateDescription" />
                                <NumberField label="Version" value-bind="$page.insertUpdateVersion" />
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
                                        onClick="addParam"
                                    />
                                </ValidationGroup>

                                <List records-bind="$insert.workflowParamNames">
                                    <div text-bind="$record"></div>
                                </List>
                            </div>
                        </div>
                        <div className="grid grid-cols-2">
                            <div>
                                <List records-bind="$insert.arrTasks">
                                    <span className="mr-2" text-bind="$record.name" />

                                    <Button
                                        icon="plus"
                                        onClick={(e, { controller, store }) => {
                                            controller.invokeMethod("addTaskToController", store.get("$record"));
                                        }}
                                    ></Button>
                                </List>
                            </div>
                            <div>
                                <List records-bind="$insert.workflowTasks">
                                    <span className="mr-2" text-bind="$record.name" />

                                    <Button
                                        className="p-0 pl-2 pr-2"
                                        onClick={(e, { controller, store }) => {
                                            controller.invokeMethod("deleteTaskFromController", store.get("$index"));
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
                                onClick={(e, { controller, store }) => {
                                    controller.invokeMethod("createWorkflowInfo");
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
