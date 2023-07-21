import { Controller } from "cx/ui";
import { List } from "cx/widgets";
import Controller from "./Controller";

export default () => (
    <cx>
        <div controller={Controller} className="border border-gray-200">
            <List
                records-bind="$page.workflows"
                mod="bordered"
                onItemClick={(e, { store }) => {
                    let currentWorkflow = store.get("$record");
                    store.set("$page.currentWorkflow", currentWorkflow);
                    store.set("$page.arrTasks", store.get("$page.currentWorkflow.tasks"));
                    store.set("$page.currentWorkflowInUndoneList", false);
                }}
            >
                <p text-tpl="{$record.name}" />
            </List>
        </div>
        <div controller={Controller} className="bg-blue-300 flex-1 p-2">
            Workflows list
        </div>
    </cx>
);
