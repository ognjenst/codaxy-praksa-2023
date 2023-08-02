import {
    Button,
    FlexCol,
    FlexRow,
    Grid,
    GridColumnConfig,
    GridRowLineConfig,
    Icon,
    LookupField,
    Switch,
    TextArea,
    TextField,
    ValidationGroup,
} from "cx/widgets";
import Controller from "./Controller";
import WorkflowTaskProperties from "./workflow-task-properties";

export default () => (
    <cx>
        <div className="relative" controller={Controller} if-expr="{$page.flagDashboard} == true">
            <span
                className="relative -top-4 left-5 bg-white p-2 text-gray-600 whitespace-nowrap"
                text-bind="$page.currentWorkflow.name"
                if-expr="{$page.undoneWorkflows}.length != 0 || {$page.workflows}.length != 0"
            />
            <Button
                if-expr="{$page.currentWorkflowInUndoneList} == true"
                className="absolute top-2 right-2 p-0 z-10 w-7"
                mod="hollow"
                onClick="deleteUndoneWorkflow"
            >
                <Icon name="trash" className="w-6 h-6 text-gray-600" />
            </Button>

            <Button
                className="absolute top-2 right-2 p-0 w-7"
                mod="hollow"
                confirm="Are you sure, you want to delete this workflow?"
                onClick="deleteWorkflow"
            >
                <Icon name="trash" className="w-6 h-6 text-gray-600" />
            </Button>

            <Button className="absolute top-2 right-12 p-0 w-7 " mod="hollow" onClick="updateWorkflow">
                <Icon name="pencil" className="w-6 h-6 text-gray-600" />
            </Button>

            <Button
                if-expr="{$page.currentWorkflowInUndoneList} == true"
                className="absolute top-2 right-20 mr-2 p-0"
                mod="hollow"
                onClick="registerWorkflow"
            >
                <Icon name="plus-circle" className="w-7 h-7 text-gray-600" />
            </Button>
        </div>

        <div className="p-10" controller={Controller} if-expr="{$page.flagDashboard} == true">
            <FlexCol>
                <TextArea
                    rows={2}
                    label="Description"
                    value-bind="$page.currentWorkflow.description"
                    readOnly
                    className="w-full"
                    emptyText="No Description"
                />
                <TextField readOnly value-bind="$page.currentWorkflow.version" className="w-full mb-2" label="Version" />
                <LookupField
                    className="w-full mb-2"
                    label="Workflow Input Parameters"
                    value-bind="$page.selectedInputParameters"
                    options-bind="$page.showInputParameters"
                />
            </FlexCol>
            <FlexRow align="center" className="h-full m-0" if-expr="{$page.flagPauseStopStatus} == true">
                <div text="Enabled" />
                <Switch rangeStyle={highlightEnabledBackgroundColor} off-bind="$page.check" />
            </FlexRow>
            <FlexRow align="center" className="h-full m-0" if-expr="{$page.currentWorkflowInUndoneList} == false">
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

            <div className="flex flex-col flex-1 mt-10">
                <div class="mt-3 md:mt-0 lg:mt-0 bg-white border border-gray-200 col-span-4 rounded-sm">
                    <div className="relative">
                        <span className="relative -top-4 left-5 bg-white p-2 text-gray-600">Tasks</span>

                        <div class="grid h-full place-items-center w-full overflow-auto">
                            <Grid
                                indexAlias="$indexTask"
                                recordAlias="$task"
                                className="tasks-grid"
                                cached
                                style={{ width: "80%" }}
                                records-bind="$page.currentWorkflow.tasks"
                                row={{
                                    line1: {
                                        columns: [
                                            {
                                                header: "Name",
                                                field: "name",
                                            },
                                            {
                                                header: {
                                                    items: (
                                                        <cx>
                                                            <cx>
                                                                <Button
                                                                    mod="hollow"
                                                                    onClick={(e, { store }) => {
                                                                        store.toggle("$page.showGridFilter");
                                                                    }}
                                                                >
                                                                    <Icon name="search" />
                                                                </Button>
                                                            </cx>
                                                        </cx>
                                                    ),
                                                },
                                                align: "right",
                                                items: (
                                                    <cx>
                                                        <cx>
                                                            <Button
                                                                mod="hollow"
                                                                onClick={(e, { store }) => {
                                                                    store.toggle("$task.flagShow");
                                                                }}
                                                            >
                                                                <Icon name="drop-down" />
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
        </div>
    </cx>
);

const highlightEnabledBackgroundColor = "background:#00FF00";
