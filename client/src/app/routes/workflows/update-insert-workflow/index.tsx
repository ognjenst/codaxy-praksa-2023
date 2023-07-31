import { Instance, LabelsLeftLayout, LabelsTopLayout, bind } from "cx/ui";
import {
    Button,
    Checkbox,
    HighlightedSearchText,
    LabeledContainer,
    List,
    NumberField,
    TextField,
    ValidationGroup,
    Window,
} from "cx/widgets";
import getController from "./Controller";
import { Bind } from "cx/src/core";

export const openInsertUpdateWindow = ({ props }) => {
    return new Promise((reslove) => {
        let w = Window.create(
            <cx>
                <Window
                    title={props.action}
                    center
                    className="p-4 h-[90vh] w-[60vw]"
                    modal
                    draggable
                    closeOnEscape
                    controller={getController(reslove, props)}
                    onDestroy={() => reslove(false)}
                >
                    <div className="flex flex-col" layout={LabelsLeftLayout}>
                        <div className="grid md:grid-cols-1 lg:grid-cols-2 gap-4">
                            <div layout={LabelsLeftLayout}>
                                <TextField
                                    label="Name"
                                    value-bind="$page.insertUpdateName"
                                    if-expr="{$page.flagShowCorrectTextField} === true"
                                    readOnly
                                />
                                <TextField
                                    label="Name"
                                    value-bind="$page.insertUpdateName"
                                    if-expr="{$page.flagShowCorrectTextField} === false"
                                />
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
                            <LabelsLeftLayout className="text-gray-600">
                                <LabeledContainer label="Search Tasks">
                                    <TextField value-bind="$page.search.query" />
                                    <Checkbox value-bind="$page.search.filter" text="Filter" className="ml-2" />
                                </LabeledContainer>
                            </LabelsLeftLayout>
                            <div className="flex gap-2">
                                <TasksList
                                    records={bind("$insert.arrTasks")}
                                    search={bind("$page.search")}
                                    buttonIcon="plus"
                                    onSelectClick={(e, { controller, store }) => {
                                        controller.invokeMethod("addTaskToController", store.get("$record"));
                                    }}
                                />
                                <TasksList
                                    records={bind("$insert.workflowTasks")}
                                    search={bind("$page.search")}
                                    buttonIcon="minus"
                                    emptyText="No tasks selected"
                                    onSelectClick={(e, { controller, store }) => {
                                        controller.invokeMethod("deleteTaskFromController", store.get("$index"));
                                    }}
                                />
                            </div>
                        </div>
                        <div putInto="footer" className="flex justify-end gap-2">
                            <Button text="Cancel" dismiss />
                            <Button
                                mod="primary"
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

interface ITasksListParams {
    records: Bind;
    search: Bind;
    buttonIcon: string;
    emptyText?: string;
    onSelectClick: (e: Event, instance: Instance) => void;
}

const TasksList = ({ records, search, buttonIcon, emptyText, onSelectClick }: ITasksListParams) => (
    <cx>
        <div className="flex-1 mt-6">
            <div className="text-gray-600 " text="All available tasks: " />
            <List
                records={records}
                filterParams={search}
                emptyText={emptyText}
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
                <div className="flex items-center justify-middle">
                    <div className="flex-1">
                        <HighlightedSearchText text-tpl="{$record.name} " query-bind="$page.search.query" />
                    </div>
                    <Button icon={buttonIcon} onClick={onSelectClick} />
                </div>
            </List>
        </div>
    </cx>
);
