import { Button, FlexRow, Grid, GridColumnConfig, GridRowLineConfig, Icon, LookupField, Switch } from "cx/widgets";
import Controller from "./Controller";
import { Svg } from "cx/svg";
import WorkflowTaskProperties from "./workflow-task-properties";
import InputParams from "../input-params";
import ConditionExecution from "../condition-execution";
import { openInsertUpdateWindow } from "../update-insert-workflow";
import { expr } from "cx/ui";
import { updateArray } from "cx/data";

export default () => (
    <cx>
        <div className="relative" controller={Controller}>
            <span className="relative -top-4 left-5 bg-white p-2 text-gray-600" text-bind="$page.currentWorkflow.name" />
            <Button
                if-expr="{$page.currentWorkflowInUndoneList} == true"
                className="absolute top-2 right-2 p-0 z-10"
                mod="hollow"
                onClick="deleteUndoneWorkflow"
            >
                <Icon name="trash" className="w-6 h-6 text-gray-600" />
            </Button>

            <Button className="absolute top-2 right-2 p-0" mod="hollow" onClick="deleteWorkflow">
                <Icon name="trash" className="w-6 h-6 text-gray-600" />
            </Button>

            <Button className="absolute top-2 right-12 p-0 " mod="hollow" onClick="updateWorkflow">
                <Icon name="refresh" className="w-6 h-6 text-gray-600" />
            </Button>

            <Button
                if-expr="{$page.currentWorkflowInUndoneList} == true"
                text="Save workflow"
                className="absolute top-4 left-4 p-2 text-gray-600"
                mod="classic"
            />
        </div>

        <div className="p-10" controller={Controller}>
            <div className="flex flex-row">
                <div className="flex flex-col">
                    <div className="flex-1">
                        <FlexRow align="center" className="h-full m-0">
                            Enabled
                        </FlexRow>
                    </div>
                    <div className="flex-1">
                        <FlexRow align="center" className="h-full m-0">
                            Status
                        </FlexRow>
                    </div>
                </div>
                <div className="flex flex-col">
                    <div className="flex-1">
                        <Switch rangeStyle={highlightEnabledBackgroundColor} off-bind="$page.check" />
                    </div>
                    <div className="flex-1">
                        <FlexRow align="center" className="h-full">
                            <Button class="rounded-full p-1 m-1">
                                <Icon name="stop-circle" className="w-6 h-6 text-gray-600" />
                            </Button>

                            <Button class="rounded-full p-1 m-1">
                                <Icon name="pause-circle" className="w-6 h-6 text-gray-600" />
                            </Button>

                            <Button class="rounded-full p-1 m-1">
                                <Icon name="play-circle" className="w-6 h-6 text-gray-600" />
                            </Button>
                        </FlexRow>
                    </div>
                </div>
            </div>

            <div className="flex flex-col flex-1 mt-10">
                <div class="mt-3 md:mt-0 lg:mt-0 bg-white border border-gray-200 col-span-4 rounded-sm">
                    <div className="relative">
                        <span className="relative -top-4 left-5 bg-white p-2 text-gray-600">Tasks</span>

                        <div class="grid h-full place-items-center">
                            <Grid
                                recordAlias="$task"
                                className="tasks-grid"
                                cached
                                style={{ width: "80%" }}
                                records-bind="$page.arrTasks"
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
