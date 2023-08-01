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
                        <ValidationGroup invalid-bind="$insert.flagWorkflow">
                            <div className="grid md:grid-cols-1 lg:grid-cols-2 gap-4">
                                <div layout={LabelsLeftLayout} className="sm:flex sm:items-center sm:justify-center">
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
                                        validationRegExp={new RegExp("^[A-Za-z]{1,}[A-Za-z0-9_]*$")}
                                        required
                                    />
                                    <TextField label="Description" value-bind="$page.insertUpdateDescription" required minLength={1} />
                                    <NumberField label="Version" value-bind="$page.insertUpdateVersion" minValue={0} required />
                                </div>

                                <div>
                                    <div className="flex items-start justify-center">
                                        <ValidationGroup invalid-bind="$insert.flagValidParamName">
                                            <TextField
                                                placeholder="Insert param name"
                                                value-bind="$insert.singleParamName"
                                                validationRegExp={new RegExp("^[A-Za-z]{1,}[A-Za-z0-9_]*$")}
                                            />

                                            <Button
                                                disabled-bind="$insert.flagValidParamName"
                                                text="Add param"
                                                className="ml-2"
                                                onClick="addParam"
                                            />
                                        </ValidationGroup>
                                    </div>

                                    <div className="flex items-center justify-center">
                                        <List records-bind="$insert.workflowParamNames">
                                            <div text-bind="$record" />
                                        </List>
                                    </div>
                                </div>
                            </div>
                            <div className="border border-gray-200 rounded-sm mt-6 p-2">
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
                                    disabled-bind="$insert.flagWorkflow"
                                    onClick={(e, { controller, store }) => {
                                        controller.invokeMethod("createWorkflowInfo");
                                    }}
                                    text="Submit"
                                />
                            </div>
                        </ValidationGroup>
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
