import {
    Button,
    FlexCol,
    FlexRow,
    Grid,
    GridColumnConfig,
    GridRowLineConfig,
    Icon,
    LabeledContainer,
    LookupField,
    Switch,
    TextArea,
    TextField,
    ValidationGroup,
} from "cx/widgets";
import Controller from "./Controller";
import WorkflowTaskProperties from "./workflow-task-properties";
import { FirstVisibleChildLayout, LabelsTopLayout, bind } from "cx/ui";

export default () => (
    <cx>
        <div className="flex flex-col gap-2 overflow-hidden h-full">
            <div className="pl-10 flex justify-between" controller={Controller} if-expr="{$page.flagDashboard} == true">
                <span
                    className="bg-white text-gray-600 whitespace-nowrap"
                    text-tpl="Workflow: {$page.currentWorkflow.name}"
                    if-expr="{$page.undoneWorkflows}.length != 0 || {$page.workflows}.length != 0"
                />
                <div className="flex gap-3">
                    <Button
                        if-expr="{$page.currentWorkflowInUndoneList} == true"
                        mod="hollow"
                        className="p-0 w-8 h-8 hover:text-blue-600"
                        onClick="registerWorkflow"
                        icon="plus"
                    />
                    <Button className="p-0 w-8 h-8 hover:text-yellow-600" mod="hollow" icon="pencil" onClick="updateWorkflow" />

                    <FirstVisibleChildLayout>
                        <Button
                            if-expr="{$page.currentWorkflowInUndoneList} == true"
                            className="p-0 w-8 h-8 hover:text-red-600"
                            mod="hollow"
                            onClick="deleteUndoneWorkflow"
                            icon="trash"
                        />
                        <Button
                            className="p-0 w-8 h-8 hover:text-red-600"
                            mod="hollow"
                            confirm="Are you sure, you want to delete this workflow?"
                            onClick="deleteWorkflow"
                            icon="trash"
                        />
                    </FirstVisibleChildLayout>
                </div>
            </div>

            <div className="pl-10 mt-4" controller={Controller} if-expr="{$page.flagDashboard} == true">
                <FlexCol>
                    <LabeledContainer label="Description">
                        <div text-bind="$page.currentWorkflow.description" className="text-slate-600 text-sm border p-2 rounded-md" />
                    </LabeledContainer>

                    <div className="flex justify-between items-end mb-2 gap-2">
                        <div className="w-24 mt-2 text-slate-700">
                            <span text="Version" className="text-md" />
                            <div text-bind="$page.currentWorkflow.version" className="text-slate-600 text-sm border p-2 rounded-md" />
                        </div>
                        <Button text="Show input parameters" className="flex-1" onClick="showInputParameters" />
                    </div>
                </FlexCol>
                <FlexRow align="center" className="m-0" if-expr="{$page.flagPauseStopStatus} == true">
                    <div text="Enabled" />
                    <Switch rangeStyle={highlightEnabledBackgroundColor} off-bind="$page.check" />
                </FlexRow>
                <FlexRow align="center" className="m-0" if-expr="{$page.currentWorkflowInUndoneList} == false">
                    <div text="Status" />
                    <Button class="rounded-full p-1 m-1" if-expr="{$page.flagPauseStopStatus} == true">
                        <Icon name="stop-circle" className="w-6 h-6 text-gray-600" />
                    </Button>

                    <Button class="rounded-full p-1 m-1" if-expr="{$page.flagPauseStopStatus} == true">
                        <Icon name="pause-circle" className="w-6 h-6 text-gray-600" />
                    </Button>

                    <Button class="rounded-full p-1 m-1" onClick="playWorkflow">
                        <Icon name="play-circle" className="w-6 h-6 text-gray-600" />
                    </Button>
                </FlexRow>

                <div className="border border-gray-200 rounded-sm mt-4 flex-1 flex flex-col h-[580px]">
                    <span className="bg-white p-2 text-gray-600" text="Tasks" />
                    <div class="flex flex-col overflow-hidden">
                        <Grid
                            indexAlias="$indexTask"
                            recordAlias="$task"
                            className="tasks-grid flex-1"
                            scrollable
                            records-bind="$page.currentWorkflow.tasks"
                            row={{
                                line1: {
                                    columns: [
                                        {
                                            header: {
                                                colSpan: 2,
                                                text: "Name",
                                            },
                                            field: "name",
                                            className: "bg-slate-200",
                                        },
                                        {
                                            align: "right",
                                            className: "bg-slate-200",
                                            items: (
                                                <cx>
                                                    <cx>
                                                        <Button
                                                            mod="hollow"
                                                            onClick={(e, { store }) => {
                                                                store.toggle("$task.flagShow");
                                                            }}
                                                        >
                                                            <Icon
                                                                name="drop-down"
                                                                class={{
                                                                    "animate duration-300": true,
                                                                    "rotate-180": bind("$task.flagShow"),
                                                                }}
                                                            />
                                                        </Button>
                                                    </cx>
                                                </cx>
                                            ),
                                        },
                                    ],
                                },
                                line2: {
                                    visible: { expr: "{$task.flagShow}" },
                                    columns: [
                                        {
                                            colSpan: 2,
                                            items: <WorkflowTaskProperties />,
                                        },
                                    ],
                                },
                            }}
                        />
                    </div>
                </div>
            </div>
        </div>
    </cx>
);

const highlightEnabledBackgroundColor = "background:#00FF00";
