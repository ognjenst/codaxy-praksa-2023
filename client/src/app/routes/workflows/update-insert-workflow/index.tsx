import { LabelsLeftLayout, LabelsTopLayout } from "cx/ui";
import { Button, Checkbox, HighlightedSearchText, List, NumberField, TextField, ValidationGroup, Window } from "cx/widgets";
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
                    <div className="grid grid-rows-3" layout={LabelsLeftLayout} styles={{ height: "620px" }}>
                        <div className="grid md:grid-cols-1 lg:grid-cols-2 gap-4">
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
                        <div className="border border-gray-200 rounded-sm mt-4 p-2">
                            <span className="relative -top-6 left-5 bg-white p-2 text-gray-600" text="Workflow Tasks" />
                            <div layout={LabelsTopLayout} className="-mt-8 text-gray-600">
                                <TextField label="Search Tasks" value-bind="$page.search.query" />
                                <Checkbox value-bind="$page.search.filter">Filter</Checkbox>
                            </div>
                            <div className="grid sm:grid-cols-1 md:grid-cols-2 mt-2">
                                <div>
                                    <div className="text-gray-600" text="All available tasks: " />
                                    <br />
                                    <List
                                        records-bind="$insert.arrTasks"
                                        filterParams-bind="$page.search"
                                        className="text-gray-600 w-full"
                                        styles={{ textAlign: "center" }}
                                        mod="bordered"
                                        onCreateFilter={(params) => {
                                            let { query, filter } = params || {};
                                            if (!filter) return () => true;
                                            if (!query) return () => true;
                                            return (record) => record.name.toLowerCase().includes(query.toLowerCase());
                                        }}
                                    >
                                        <HighlightedSearchText text-tpl="{$record.name} " query-bind="$page.search.query" />
                                        <Button
                                            icon="plus"
                                            onClick={(e, { controller, store }) => {
                                                controller.invokeMethod("addTaskToController", store.get("$record"));
                                            }}
                                        ></Button>
                                    </List>
                                </div>
                                <div>
                                    <div className="text-gray-600" text="All choosen tasks: " />
                                    <br />
                                    <List
                                        records-bind="$insert.workflowTasks"
                                        className="text-gray-600 w-full md:!border-l-0 sm:mt-2 md:mt-0"
                                        mod="bordered"
                                        styles={{ textAlign: "center" }}
                                    >
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
